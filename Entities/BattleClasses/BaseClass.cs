using System;
using System.Collections.Generic;
using SharpBattle.Entities;

namespace SharpBattle
{
    public class BaseClass
    {
        protected double Hp = 20.00;
        protected double Str = 10.00;
        protected double Dmg {get; private set;}
        protected double Def = 2.0;
        protected double Mag = 4.0;
        protected double Hly = 2.0;
        protected double Lck = 1.0;

        #region GET/SET PROPERTIES
        public BaseClass() {
            Dmg = Str * 1.1;
        }
        public double HP
        {
            get => Hp;
            set { Hp = value; }
        }
        public double STR
        {
            get => Str;
            set => Str = value;
        }
        public double DEF
        {
            get => Def;
            set { Def = value; }
        }
        public double MAG
        {
            get => Mag;
            set { Mag = value; }
        }
        public double HLY
        {
            get => Hly;
            set { Hly = value; }
        }
        public double LCK
        {
            get => Lck;
            set { Lck = value; }
        }
        /*
        public double MANA
        {
            get => MAG * 2;
            set { MANA = value; }
        }
        */
        #endregion GET/SET PROPERTIES

        #region AUXILIARY METHODS
        public static string ClassName(BaseClass _class)
        {
            return _class.GetType().Name;
        }

        public static void OverridePlayerStats(Player player, BaseClass obj)
        {
            player.HP = obj.HP;
            player.STR = obj.STR;
            player.DEF = obj.DEF;
            player.MAG = obj.MAG;
            player.HLY = obj.HLY;
            player.LCK = obj.LCK;
        }
        #endregion AUXILIARY METHODS
    }
}