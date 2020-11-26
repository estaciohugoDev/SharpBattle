using System;
using System.Threading.Channels;
using SharpBattle.Entities;

namespace SharpBattle
{
    public class Enemy
    {
        public readonly string Name;
        public double Health = 10.00; 
        private double Damage { get; set; }
        public bool IsTurn;

        public Enemy()
        {
            string[] enemList = { "Thief", "Furry", "Communist", "Slime", "Skeleton swordsman", "Wolf", "Cave Troll" };
            Random rand = new Random();
            int randName = rand.Next(enemList.Length);
            Name = enemList[randName];
        }

        public void EnemyAction(Player player)
        {
            var action = 1;
            //Action is attack by default since rest TBI

            if (!(Health > 0))
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
            if (player.Health <= 0)
            {
                Console.WriteLine($"{Name} beat {player.Name}!");
                return 0;
            }

            var rnd = new Random();
            Damage = rnd.Next(2, 20);
            player.Health -= Damage;
            Console.WriteLine($"{Name} attacks {player.Name} causing {Damage} damage!");
            return Damage;
        }

        public string EnemyInfo()
        {
            return "\n - - - - - - - - - - - - - - - - - - -\n" +
            $"           Enemy INFO \n\nName: {Name}\nHP: {Health:F2}"
            + "\n - - - - - - - - - - - - - - - - - - -\n";
        }
    }
}