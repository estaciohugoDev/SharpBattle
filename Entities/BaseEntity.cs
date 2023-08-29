using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBattle.Entities
{
    public class BaseEntity : IBaseActions
    {
        public string Name;
        public double HP;
        public double DMG;
        public double STR = 10.00;
        public double DEF = 5.0;
        public double MAG = 4.0;
        public double HLY = 3.0;
        public double LCK = 1.5;

        public double Attack(BaseEntity target)
        {
            var rnd = new Random();
            STR = rnd.Next(5, 40);
            target.HP -= STR;
            Console.WriteLine($"{Name} attacks {target.Name} causing {STR} DMG!");
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
        public double Use()
        {
            throw new NotImplementedException();
        }
    }
}