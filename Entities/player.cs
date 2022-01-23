using System;
using SharpBattle.Entities.Enums;
using System.Collections.Generic;
using SharpBattle.Entities;
using SharpBattle.Util;

namespace SharpBattle
{
    public class Player : BaseClass
    {
        public static string PlayerName;
        public BaseClass PlayerClass;
        private Stances FightingStances { get; set; }
        public bool IsTurn = true;

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
                    System.Console.WriteLine("What class, mate?");
                    Utilities.Wait();
                    SelectClass(this);
                }
            }
        }
        public string Name
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

        // ---------------- METHODS ---------------

        public void NextAction(Enemy enemy)
        {
            while (IsTurn)
            {
                Console.WriteLine(Utilities.WelcomeBattle());
                Console.WriteLine(PlayerInfo());
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
                            Knight knightVar = Knight.GetKnightBuffer();
                            knightVar.ListSkills();
                            knightVar.ChooseSkill(this, enemy);
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
                        IsTurn = false;
                        enemy.IsTurn = true;
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
                SelectClass(this);
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
                "3 - Phoenix Stance (+POW) \n" +
                "4 - Panda Stance (+HLY) \n");
            Console.Write("Switching to: ");
            var choice = int.Parse(Console.ReadLine());

            try
            {
                switch (choice)
                {
                    case 1:
                        this.FightingStances = (Stances)choice;
                        this.DMG += 10;
                        Console.WriteLine($"{this.Name} changed stance to: {this.FightingStances}");
                        break;

                    case 2:
                        this.FightingStances = (Stances)choice;
                        this.HP += 20;
                        Console.WriteLine($"{this.Name} changed stance to: {this.FightingStances}");
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
        private void Scan(Enemy enemy)
        {
            Console.WriteLine($"{Name} scans {enemy.Name}!");
            Console.WriteLine(enemy.EnemyInfo());
        }
        public void SelectClass(Player player)
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
                        System.Console.WriteLine($"{player.Name} is now a Knight!");
                        player.Class = new Knight();
                        Knight.OverridePlayerStats(this);
                        break;
                    default:
                        System.Console.WriteLine("Not implemented.");
                        Utilities.Wait();
                        SelectClass(this);
                        break;
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Invalid input: {e.Message}");
                Utilities.Wait();
                SelectClass(this);
            }

        }

        public static List<string> CondensedPlayerInfo(Player player)
        {
            List<string> playerInfo = new List<string>
            {
                player.Name,
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
        private string PlayerInfo()
        {
            return "\n - - - - - - - - - - - - - - - - - - -\n" +
            $"           CHARACTER INFO \n\nName: {PlayerName}\nClass: {this.Class.GetType().Name}\nHP: {HP:F2}"
            + "\n - - - - - - - - - - - - - - - - - - -\n";
        }
    }
}
