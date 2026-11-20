using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using SharpBattle.Entities.Enums;
using SharpBattle.Util;

namespace SharpBattle.Entities
{
    public class BaseEntity : IBaseEntity
    {
        public string Name { get; set; }
        public int HP { get; private set; } = 10;
        public int STR;
        public int DEF;
        public int MAG;
        public int HLY;
        public double LCK;
        public FightingStances Stance { get; private set; } = FightingStances.Neutral;

        public BaseClass Class;
        public Random Roll { get; private set; }
        public bool IsDead => HP <= 0;
        public int RollDamage() => Roll.Next(1, STR + 1);
        public void SwitchStance(FightingStances stance) => Stance = stance switch
        {
            FightingStances.Attacking => FightingStances.Attacking,
            FightingStances.Defending => FightingStances.Defending,
            FightingStances.Neutral => FightingStances.Neutral,
            _ => Stance
        };

        public BaseEntity()
        {
            Roll = new();
        }
        public int TakeDamage(int damage)
        {
            if(Stance == FightingStances.Defending)
            {
                damage = (int)(damage * 0.8);
            }
            var mitigated = Math.Max(0, damage - DEF);
            HP = Math.Max(0, HP - mitigated);
            return mitigated;
        }
        public int Attack(BaseEntity target)
        {
            if(Stance == FightingStances.Attacking)
            {
                STR = (int)(STR * 1.2);
            }
            var dmg = target.TakeDamage(RollDamage());
            Console.WriteLine($"{Name} attacks {target.Name} causing {dmg} DMG!");
            if (target.HP <= 0)
            {
                Console.WriteLine($"{Name} beat {target.Name}!");
                return 0;
            }
            return dmg;
        }

        public int Cast(BaseEntity target)
        {
            throw new NotImplementedException();
        }

        public int Guard()
        {
            throw new NotImplementedException();
        }
        public void Use()
        {
            throw new NotImplementedException();
        }
        

        public void OverrideEntityStats(BaseClass param)
        {
            HP = param.HP;
            STR = param.STR;
            DEF = param.DEF;
            MAG = param.MAG;
            HLY = param.HLY;
            LCK = param.LCK;
        }

        public void ResetEntityStats()
        {
            HP = 10;
            STR = 1;
            DEF = 1;
            MAG = 1;
            HLY = 1;
            LCK = 0.5;
        }

        public string EntityInfo()
        {
            return "\n - - - - - - - - - - - - - - - - - - -\n" +
            $"           INFO \n\nName: {Name}\nHP: {HP:F2}"
            + "\n - - - - - - - - - - - - - - - - - - -\n";
        }

    }
}