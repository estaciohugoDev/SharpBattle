using System;
using SharpBattle.Entities.Enums;

namespace SharpBattle.Player
{
    public class Player
    {
        private static string _name;
        public double Health = 50.00;
        private double Damage { get; set; }
        private Stances fightingStances { get; set; }

        public bool IsTurn = true;

        public Player() { }

        public Player(string name) : this()
        {
            _name = name;
        }

        public string Name
        {
            get => _name;
            set
            {
                if (value != null && value.Length > 1)
                {
                    _name = value;
                }
                else
                {
                    _name = "Player";
                }
            }
        }

        // ---------------- METHODS ---------------

        public void NextAction(Enemy.Enemy enemy)
        {
            while (IsTurn)
            {
                Console.WriteLine(Run.WelcomeBattle());
                Console.WriteLine(PlayerInfo());
                Console.WriteLine("----> Battle Actions <----\n " +
                "1 - Attack!\n " +
                "2 - Scan!\n " +
                "3 - Switch Stance!\n " +
                "4 - Items!\n " +
                "5 - Talk!\n " +
                "6 - Run!\n ");

                Console.Write($"{Name}'s turn: ");
                var action = 0;
                while (action is 0)
                {
                    action = int.Parse(Console.ReadLine());
                    if (action == 0) continue;
                    Console.WriteLine("Values between 1 - 6 ONLY!");
                    Console.Clear();
                }

                switch (action)
                {
                    case 1:
                        Attack(enemy);
                        Run.Wait();
                        Console.Clear();
                        IsTurn = false;
                        enemy.IsTurn = true;
                        break;

                    case 2:
                        Scan(enemy);
                        Run.Wait();
                        Console.Clear();
                        IsTurn = false;
                        enemy.IsTurn = true;
                        break;

                    case 3:
                        SwitchStance();
                        Run.Wait();
                        Console.Clear();
                        IsTurn = false;
                        enemy.IsTurn = true;
                        break;

                    default:
                        Console.WriteLine("Not implemented yet");
                        Run.Wait();
                        Console.Clear();
                        IsTurn = false;
                        enemy.IsTurn = true;
                        break;
                }
            }
        }
        public void CreatePlayer()
        {
            Console.Clear();
            Console.Write("\nPlayer name: ");
            _name = Console.ReadLine();
            Console.Clear();
        }

        private double Attack(Enemy.Enemy enemy)
        {
            var rnd = new Random();
            Damage = rnd.Next(5, 40);
            enemy.Health -= Damage;
            Console.WriteLine($"{Name} attacks {enemy.Name} causing {Damage} damage!");
            if (enemy.Health <= 0)
            {
                Console.WriteLine($"{Name} beat {enemy.Name}!");
                return 0;
            }
            return Damage;
        }

        private void SwitchStance()
        {
            Console.Clear();
            Console.WriteLine(
                "Choose one of the following: \n" +
                "1 - Tiger Stance(+DMG) \n" +
                "2 - Turtle Stance (+DEF) \n" +
                "3 - Phoenix Stance (+POW) \n" +
                "4 - Panda Stance (+HLY) \n");
            Console.Write("Switching to: ");
            var choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    this.fightingStances = (Stances)choice;
                    this.Damage += 10;
                    Console.WriteLine($"{this.Name} changed stance to: {this.fightingStances}");
                    break;

                case 2:
                    this.fightingStances = (Stances)choice;
                    this.Health += 10;
                    Console.WriteLine($"{this.Name} changed stance to: {this.fightingStances}");
                    break;

                default:
                    Console.WriteLine("1 - 4 ONLY!");
                    SwitchStance();
                    break;
            }
        }

        private void Scan(Enemy.Enemy enemy)
        {
            Console.WriteLine($"{Name} scans {enemy.Name}!");
            Console.WriteLine(enemy.EnemyInfo());
        }
        private string PlayerInfo()
        {
            return "\n - - - - - - - - - - - - - - - - - - -\n" +
            $"           CHARACTER INFO \n\nName: {_name}\nHP: {Health:F2}"
            + "\n - - - - - - - - - - - - - - - - - - -\n";
        }
    }
}
