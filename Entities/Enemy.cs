using System;
using System.Data;
using System.Threading.Channels;
using SharpBattle.Entities;
using SharpBattle.Util;

namespace SharpBattle
{
    public class Enemy : BaseEntity
    {
        public bool IsTurn;
        public Enemy()
        {
            string[] enemList = { "Thief", "Furry", "Bandit", "Slime", "Skeleton", "Wolf", "Troll" };
            int randomName = Roll.Next(enemList.Length);
            STR = Roll.Next(1,5);
            DEF = Roll.Next(1,5);
            Name = enemList[randomName];
        }

        #region METHODS 

        public void Actions(Player player)
        {
            var action = 1;
            //Action is attack by default since rest TBI

            if (!(HP > 0))
            {
                Console.WriteLine($"{player.Name} beat {Name}!");
                return;
            }
            if (!IsTurn) return;
            switch (action)
            {
                case 1:
                    Attack(player);
                    Utilities.Wait();
                    Console.Clear();
                    IsTurn = false;
                    player.IsTurn = true;
                    break;
            }
        }
        private double Attack(Player player)
        {
            if (player.HP <= 0)
            {
                Console.WriteLine($"{Name} beat {player.Name}!");
                return 0;
            }

            DMG = Roll.Next(1,STR);
            player.TakeDamage(DMG);
            Console.WriteLine($"{Name} attacks {player.Name} causing {DMG} damage!");
            return DMG;
        }

        #endregion METHODS 

        #region AUXILIARY METHODS
        public static Enemy NewEnemy()
        {
            return new Enemy();
        }
        public string EnemyInfo()
        {
            return "\n - - - - - - - - - - - - - - - - - - -\n" +
            $"           Enemy INFO \n\nName: {Name}\nHP: {HP:F2}"
            + "\n - - - - - - - - - - - - - - - - - - -\n";
        }

        #endregion AUXILIARY METHODS
    }
}