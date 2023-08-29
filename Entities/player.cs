using System;
using SharpBattle.Entities.Enums;
using System.Collections.Generic;
using SharpBattle.Entities;
using SharpBattle.Util;

namespace SharpBattle
{
    public class Player : BaseEntity
    {
        private static string PlayerName;
        public BaseClass PlayerClass;
        public string ClassName { get; private set;}
        private FightingStances Stances { get; set; }
        public bool IsTurn = true;
        public Enemy Enemy { get; set; }
        public Player() { CreatePlayer(); }

        public BaseClass Class
        {
            get => PlayerClass;
            set
            {
                if (value != null)
                    PlayerClass = value;
                else
                {
                    System.Console.WriteLine("Invalid Class.");
                    Utilities.Wait();
                    SelectClass();
                }
            }
        }
        public static new string Name
        {
            get => PlayerName;
            set
            {
                if (value != null && value.Length > 1)
                    PlayerName = value;
                else
                    PlayerName = "Player";
            }
        }
        #region METHODS

        public void BattleActions(Enemy enemy)
        {
            while (IsTurn)
            {
                Utilities.Welcome();
                Console.WriteLine(CharacterSheet());
                Console.WriteLine("----> Battle Actions <----\n " +
                "1 - Attack!\n " +
                "2 - Scan!\n " +
                "3 - Switch Stance!\n " +
                "4 - Skills!\n " +
                "5 - Talk! (NOT IMPLEMENTED YET)\n " +
                "6 - Run! (NOT IMPLEMENTED YET)\n ");

                Console.Write($"{Name}'s turn: ");
                var action = 0;
                while (action is 0)
                {
                    action = int.Parse(Console.ReadLine());
                    if (action == 0) continue;
                    Console.WriteLine("Values between 1 - 6 ONLY!");
                    Console.Clear();
                }

                switch (action)
                {
                    case 1:
                        Attack(enemy);
                        Utilities.Wait();
                        Console.Clear();
                        IsTurn = false;
                        enemy.IsTurn = true;
                        break;
                    case 2:
                        Scan(enemy);
                        Utilities.Wait();
                        Console.Clear();
                        IsTurn = false;
                        enemy.IsTurn = true;
                        break;

                    case 3:
                        SwitchStance();
                        Utilities.Wait();
                        Console.Clear();
                        IsTurn = false;
                        enemy.IsTurn = true;
                        break;

                    case 4:
                        if (BaseClass.ClassName(PlayerClass) == "Knight")
                        {
                            Knight knightVar = (Knight)Class;
                            knightVar.ListSkills();
                            Knight.ChooseSkill(this, enemy);
                            Utilities.Wait();
                            Console.Clear();
                            IsTurn = false;
                            enemy.IsTurn = true;
                        }
                        break;

                    default:
                        Console.WriteLine("Not implemented yet");
                        Utilities.Wait();
                        Console.Clear();
                        IsTurn = true;
                        enemy.IsTurn = false;
                        break;
                }
            }
        }
        public void CreatePlayer()
        {
            Console.Write("\nPlayer name: ");
            try
            {
                PlayerName = Console.ReadLine();
                SelectClass();
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Invalid input: {e.Message}");
                Utilities.Wait();
                CreatePlayer();
            }
            Console.Clear();
        }
        private double Attack(Enemy enemy)
        {
            var rnd = new Random();
            DMG = rnd.Next(5, 40);
            enemy.HP -= DMG;
            Console.WriteLine($"{Name} attacks {enemy.Name} causing {DMG} DMG!");
            if (enemy.HP <= 0)
            {
                Console.WriteLine($"{Name} beat {enemy.Name}!");
                return 0;
            }
            return DMG;
        }
        private void SwitchStance()
        {
            Console.Clear();
            Console.WriteLine(
                "Choose one of the following: \n" +
                "1 - Tiger Stance(+DMG) \n" +
                "2 - Turtle Stance (+DEF) \n" +
                "3 - Phoenix Stance (+MAG) \n" +
                "4 - Panda Stance (+HLY) \n");
            Console.Write("Switching to: ");
            var choice = int.Parse(Console.ReadLine());

            try
            {
                switch (choice)
                {
                    case 1:
                        Stances = (FightingStances)choice;
                        DMG += 1.1;
                        Console.WriteLine($"{Name} changed stance to: {Stances}");
                        break;

                    case 2:
                        Stances = (FightingStances)choice;
                        HP += 20;
                        Console.WriteLine($"{Name} changed stance to: {Stances}");
                        break;

                    default:
                        Console.WriteLine("1 - 4 ONLY!");
                        Utilities.Wait();
                        SwitchStance();
                        break;
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Invalid input: {e.Message}");
                Utilities.Wait();
                SwitchStance();
            }
        }
        private static void Scan(Enemy enemy)
        {
            Console.WriteLine($"{Name} scans {enemy.Name}!");
            Console.WriteLine(enemy.EnemyInfo());
        }
        public void SelectClass()
        {
            Console.Clear();
            Console.WriteLine("-----> Please select a class <----- ");
            Console.WriteLine("1 - Knight (DMG/DEF)\n2 - Rogue (NOT IMPLEMENTED YET)");

            System.Console.Write("Choose class: ");
            var choice = int.Parse(Console.ReadLine());

            try
            {
                switch (choice)
                {
                    case 1:
                        Class = new Knight();
                        Console.WriteLine($"{Name} is now a {Class.GetType().Name}!");
                        BaseClass.OverridePlayerStats(this, Class);
                        break;
                    default:
                        Console.WriteLine("Not implemented.");
                        Utilities.Wait();
                        SelectClass();
                        break;
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Invalid input: {e.Message}");
                Utilities.Wait();
                SelectClass();
            }
        }

        public string EnemyFound()
        {
            return $"{Name} finds a {Enemy.Name}, BATTLE START!";
        }

        #endregion METHODS

        #region AUXILIARY METHODS
        public static List<string> PlayerInfo (Player player)
        {
            List<string> playerInfo = new()
            {
                Name,
                player.Class.GetType().Name,
                player.HP.ToString(),
                player.DMG.ToString(),
                player.DEF.ToString(),
                player.MAG.ToString(),
                player.HLY.ToString(),
                player.LCK.ToString()
            };

            return playerInfo;
        }
        private string CharacterSheet()
        {
            return "\n - - - - - - - - - - - - - - - - - - -\n" +
            $"           CHARACTER INFO \n\nName: {PlayerName}\nClass: {this.Class.GetType().Name}\nHP: {HP:F2}\nStance: {Stances}\n"
            + "\n - - - - - - - - - - - - - - - - - - -\n";
        }
        #endregion AUXILIARY METHODS
    }
}
