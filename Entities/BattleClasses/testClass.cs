using System;
using System.Collections.Generic;
using SharpBattle.Entities;
using SharpBattle.Util;

namespace SharpBattle
{
    public static class TestClass
    {
        public static double _hp = 20.00;
        public static double _dmg = 10.00;
        public static double _def = 2.0;
        public static double _mag = 4.0;
        public static double _hly = 2.0;
        public static double _lck = 1.5;

        static TestClass() { }

        public static void SelectClass(Player player)
        {
            Console.Clear();
            Console.WriteLine("-----> Please select a class <----- ");
            Console.WriteLine("1 - Knight (DMG/DEF)\n2 - Rogue (DMG/LCK)");

            System.Console.Write("Choose class: ");
            var choice = int.Parse(Console.ReadLine());
            //Object chosenClass;
            switch (choice)
            {
                case 1:
                    System.Console.WriteLine($"{Player.Name} is now a Knight!");
                    player.Class = new Knight();
                    break;
                //return chosenClass;
                default:
                    System.Console.WriteLine("Not implemented.");
                    Utilities.Wait();
                    break;
                    //return SelectClass(player);

            }
        }

        public static Object GetPlayerClass(Player player)
        {
            if (player.Class.GetType().Name == "Knight")
            {
                Knight knightVar = new();
                return knightVar;
            }
            else
                return null;
        }
    }
}