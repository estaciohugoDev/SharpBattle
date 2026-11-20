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
            string[] enemList = { "Bandit", "Slime", "Skeleton", "Wolf", "Orc" };
            int randomName = Roll.Next(enemList.Length);
            STR = Roll.Next(1,5);
            DEF = Roll.Next(1,5);
            Name = enemList[randomName];
        }

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

        public static Enemy NewEnemy()
        {
            return new Enemy();
        }
    }
}