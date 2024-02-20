using System;
using SharpBattle.Entities.Enums;
using System.Collections.Generic;
using SharpBattle.Entities;
using SharpBattle.Util;

namespace SharpBattle
{ 
    public class Player : BaseEntity
    {
        #region PROPERTIES/CTOR
        public FightingStances Stances { get ; private set; }
        public bool IsTurn = true;
        public Player() { CreatePlayer();}
        #endregion

        #region METHODS
        private void PlayerActions(Enemy target)
        {
            //TODO: 1) Implement option to Run from battle.
            //      2) Implement option to talk to targets.  
            
            var action = Menus.PlayerActions();
            switch (action)
            {
                case 1:
                    Attack(target);
                    Utilities.Wait();
                    Console.Clear();
                    IsTurn = false;
                    target.IsTurn = true;
                    break;
                case 2:
                    Scan(target);
                    Utilities.Wait();
                    Console.Clear();
                    IsTurn = false;
                    target.IsTurn = true;
                    break;
                case 3:
                    SwitchStance();
                    Utilities.Wait();
                    Console.Clear();
                    IsTurn = false;
                    target.IsTurn = true;
                    break;
                case 4:
                    if (BaseClass.ClassName(Class) == "Knight")
                    {
                        Knight knightVar = (Knight)Class;
                        knightVar.ListSkills();
                        Knight.ChooseSkill(this, target);
                        Utilities.Wait();
                        Console.Clear();
                        IsTurn = false;
                        target.IsTurn = true;
                    }
                    break;
                default:
                    Console.WriteLine("Values between 1 - 4 ONLY!");
                    Utilities.Wait();
                    Console.Clear();
                    PlayerActions(target);
                    break;
            }
        }
        public void Actions(Enemy target)
        {
            while (IsTurn)
            {
                Console.WriteLine(PlayerSheet());
                PlayerActions(target);
            }
        }
        private Player CreatePlayer()
        {
            Console.Write("\nPlayer name: ");
            try
            {
                Name = Console.ReadLine().ToString();
                BaseClass.SelectClass(this);
                return default;
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Invalid input: {e.Message}");
                Utilities.Wait();
                CreatePlayer();
                return default;
            }
        }
        private void SwitchStance()
        {
            Console.Clear();
            Console.WriteLine(
                "Choose one of the following: \n" +
                "1 - Aggressive (+DMG) \n" +
                "2 - Defensive (+DEF) \n");
            Console.Write("Switching to: ");
            int choice = int.Parse(Console.ReadLine());

            try
            {
                switch (choice)
                { 
                    case 1:
                        Stances = (FightingStances)choice;
                        DMG *= (int)1.1;
                        Console.WriteLine($"{Name} changed stance to: {Stances}");
                        break;

                    case 2:
                        Stances = (FightingStances)choice;
                        DEF *= (int)1.2;
                        Console.WriteLine($"{Name} changed stance to: {Stances}");
                        break;

                    default:
                        Console.WriteLine("1 - 2 ONLY!");
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
        
        #endregion 
        #region AUXILIARY METHODS
        private void Scan(Enemy enemy)
        {
            Console.WriteLine($"{Name} scans {enemy.Name}!");
            Console.WriteLine(enemy.EnemyInfo());
        }
        public static List<string> PlayerInfo (Player player)
        {
            return
            [
                player.Name,
                player.Class.GetType().Name,
                player.HP.ToString(),
                player.DMG.ToString(),
                player.DEF.ToString(),
                player.MAG.ToString(),
                player.HLY.ToString(),
                player.LCK.ToString()
            ];
        }
        private string PlayerSheet()
        {
            return "\n - - - - - - - - - - - - - - - - - - -\n" +
            $"           CHARACTER INFO \n\nName: {Name}\nClass: {Class.Name}\nHP: {HP:F2}\nStance: {Stances}\n"
            + "\n - - - - - - - - - - - - - - - - - - -\n";
        }
        #endregion
    }
}
