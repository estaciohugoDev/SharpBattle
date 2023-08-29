using System;
using System.Threading.Channels;
using SharpBattle.Entities;
using SharpBattle.Util;

namespace SharpBattle
{
    public class Enemy : BaseEntity
    {
        protected new double DMG { get; set; }
        public bool IsTurn;
        public Enemy()
        {
            string[] enemList = { "Thief", "Furry", "Communist", "Slime", "Skeleton swordsman", "Wolf", "Cave Troll" };
            Random rand = new();
            int randName = rand.Next(enemList.Length);
            Name = enemList[randName];
            DMG = STR/2;
        }

        #region METHODS 

        public void EnemyAction(Player player)
        {
            var action = 1;
            //Action is attack by default since rest TBI

            if (!(HP > 0))
            {
                Console.WriteLine($"{Player.Name} beat {Name}!");
                return;
            }
            if (!IsTurn) return;
            switch (action)
            {
                case 1:
                    EnemyAttack(player);
                    Utilities.Wait();
                    Console.Clear();
                    IsTurn = false;
                    player.IsTurn = true;
                    break;
            }
        }
        private double EnemyAttack(Player player)
        {
            if (player.HP <= 0)
            {
                Console.WriteLine($"{Name} beat {Player.Name}!");
                return 0;
            }

            var rnd = new Random();
            DMG = rnd.Next(2, 20);
            player.HP -= DMG;
            Console.WriteLine($"{Name} attacks {Player.Name} causing {DMG} damage!");
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