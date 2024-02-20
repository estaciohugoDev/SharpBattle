using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using SharpBattle.Util;

namespace SharpBattle.Entities
{
    public class BaseEntity : IBaseActions
    {
        public string Name {get; set;}
        public int HP {get; private set;} = 10; 
        public int DMG;
        public int STR;
        public int DEF;
        public int MAG;
        public int HLY;
        public double LCK;
        public BaseClass Class;
        public Random Roll { get; private set; }
        public BaseEntity()
        {
            Roll = new();
        }
        public double Attack(BaseEntity target)
        {
            DMG = Roll.Next(2, STR);
            target.TakeDamage(DMG);
            Console.WriteLine($"{Name} attacks {target.Name} causing {DMG} DMG!");
            if (target.HP <= 0)
            {
                Console.WriteLine($"{Name} beat {target.Name}!");
                return 0;
            }
            return STR;
        }

        public double Cast(BaseEntity target)
        {
            throw new NotImplementedException();
        }

        public double Guard()
        {
            throw new NotImplementedException();
        }
        public void Use()
        {
            throw new NotImplementedException();
        }
        public void TakeDamage(int damage)
        {
            HP -= damage - DEF;
        }

        public void OverrideEntityStats(BaseClass obj)
        {
            HP = obj.HP;
            STR = obj.STR;
            DEF = obj.DEF;
            MAG = obj.MAG;
            HLY = obj.HLY;
            LCK = obj.LCK;
        }
        public string GetEntityName()
        {
            return Name;
        }
    }
}