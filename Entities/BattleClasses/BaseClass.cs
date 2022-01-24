using System;
using System.Collections.Generic;
using SharpBattle.Entities;

namespace SharpBattle
{
    public class BaseClass
    {
        protected double _hp = 20.00;
        protected double _dmg = 10.00;
        protected double _def = 2.0;
        protected double _mag = 4.0;
        protected double _hly = 2.0;
        protected double _lck = 1.0;

        #region GET/SET PROPERTIES
        public BaseClass() { }

        public double HP
        {
            get => _hp;
            set { _hp = value; }
        }
        public double DMG
        {
            get => _dmg;
            set { _dmg = value; }
        }
        public double DEF
        {
            get => _def;
            set { _def = value; }
        }
        public double MAG
        {
            get => _mag;
            set { _mag = value; }
        }
        public double HLY
        {
            get => _hly;
            set { _hly = value; }
        }
        public double LCK
        {
            get => _lck;
            set { _lck = value; }
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

        public void OverridePlayerStats(Player player, BaseClass obj)
        {
            player.HP = obj.HP;
            player.DMG = obj.DMG;
            player.DEF = obj.DEF;
            player.MAG = obj.MAG;
            player.HLY = obj.HLY;
            player.LCK = obj.LCK;
        }
        #endregion AUXILIARY METHODS
    }
}