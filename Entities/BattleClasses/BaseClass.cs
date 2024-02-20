using System;
using System.Collections.Generic;
using SharpBattle.Entities;
using SharpBattle.Util;

namespace SharpBattle
{
    public class BaseClass
    {
        protected int Hp = 10;
        protected int Str = 10;
        protected int Def = 5;
        protected int Mag = 2;
        protected int Hly = 2;
        protected double Lck = 1.0;
        public string Name;
        #region GET/SET PROPERTIES
        public int HP
        {
            get => Hp;
            set { Hp = value; }
        }
        public int STR
        {
            get => Str;
            set => Str = value;
        }
        public int DEF
        {
            get => Def;
            set { Def = value; }
        }
        public int MAG
        {
            get => Mag;
            set { Mag = value; }
        }
        public int HLY
        {
            get => Hly;
            set { Hly = value; }
        }
        public double LCK
        {
            get => Lck;
            set { Lck = value; }
        }
        #endregion GET/SET PROPERTIES

        #region AUXILIARY METHODS
        public static string ClassName(BaseClass _class)
        {
            return _class.Name;
        }
        public static void SelectClass(BaseEntity entity)
        {
            Console.Clear();
            Console.WriteLine("-----> Select a Class <----- ");
            Console.WriteLine("1 - Knight (DMG/DEF)\n");
            System.Console.Write("Choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            try
            {
                switch (choice)
                {
                    case 1:
                        entity.Class = new Knight();
                        Console.WriteLine($"{entity.Name} is now a {entity.Class.Name}!");
                        entity.OverrideEntityStats(entity.Class);
                        break;
                    default:
                        Console.WriteLine("Select one of the options.");
                        Utilities.Wait();
                        SelectClass(entity);
                        break;
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Invalid input: {e.Message}");
                Utilities.Wait();
                SelectClass(entity);
            }
        }
        #endregion AUXILIARY METHODS
    }
}