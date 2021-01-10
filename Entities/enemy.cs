using System;
using System.Threading.Channels;
using SharpBattle.Entities;

namespace SharpBattle
{
    public class Enemy
    {
        public readonly string Name;
        public double HP = 10.00; 
        private double DMG { get; set; }
        private double DEF { get; set; }
        private double MAG { get; set; }
        private double HLY { get; set; }
        public bool IsTurn;

        public Enemy()
        {
            string[] enemList = { "Thief", "Furry", "Communist", "Slime", "Skeleton swordsman", "Wolf", "Cave Troll" };
            Random rand = new Random();
            int randName = rand.Next(enemList.Length);
            Name = enemList[randName];
        }

        // ---------------- METHODS ---------------

        public void EnemyAction(Player player)
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
                    EnemyAttack(player);
                    Run.Wait();
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
                Console.WriteLine($"{Name} beat {player.Name}!");
                return 0;
            }

            var rnd = new Random();
            DMG = rnd.Next(2, 20);
            player.HP -= DMG;
            Console.WriteLine($"{Name} attacks {player.Name} causing {DMG} damage!");
            return DMG;
        }
        public string EnemyFound(Player player, Enemy enemy)
        {
            return $"{player.Name} finds a {enemy.Name}, BATTLE START!";
        }
        public static Enemy NewEnemy()
        {
            var enemy = new Enemy();

            return enemy;
        }
        public string EnemyInfo()
        {
            return "\n - - - - - - - - - - - - - - - - - - -\n" +
            $"           Enemy INFO \n\nName: {Name}\nHP: {HP:F2}"
            + "\n - - - - - - - - - - - - - - - - - - -\n";
        }
    }
}