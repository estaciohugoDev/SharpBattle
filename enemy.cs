using System;
using System.Threading.Channels;

namespace SharpBattle.Enemy
{
    public class Enemy
    {
        public readonly string Name;
        public double Health = 10.00;
        private double Damage { get; set; }

        public bool IsTurn;

        public Enemy()
        {
            string[] enemList = { "Larryzito alts", "Furry", "Antifa", "Dannygurr", "Fake Viewer", "La creatura", "POP Monster" };
            Random rand = new Random();
            int randName = rand.Next(enemList.Length);
            Name = enemList[randName];
        }

        public Enemy(string name)
        {
            Name = name;
        }

        public void EnemyAction(Player.Player player)
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

        private double EnemyAttack(Player.Player player)
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
            $"           ENEMY INFO \n\nName: {Name}\nHP: {Health:F2}"
            + "\n - - - - - - - - - - - - - - - - - - -\n";
        }
    }
}