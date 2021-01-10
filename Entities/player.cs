using System;
using SharpBattle.Entities.Enums;
using SharpBattle.Entities;

namespace SharpBattle
{
    public class Player : BaseClass
    {
        private static string PlayerName;
        public BaseClass PlayerClass;
        private Stances fightingStances { get; set; }
        public bool IsTurn = true;
        //public Type[] paramsType = new Type[2]; 

        public Player() { CreatePlayer(); }

        /*public Player(string name) : this()
        {
            PlayerName = name;
            this.CreatePlayer();

        }*/
        public BaseClass Class
        {
            get => PlayerClass;
            set
            {
                if(value != null)
                    PlayerClass = value;
                else
                {
                    System.Console.WriteLine("What class, mate?");
                    PlayerClass.GetType().GetMethod("SelectClass");
                }
            }
        }
        public string Name
        {
            get => PlayerName;
            set
            {
                if (value != null && value.Length > 1)
                    PlayerName = value;
                else
                    PlayerName = "Player";
            }
        }

        // ---------------- METHODS ---------------

        public void NextAction(Enemy enemy)
        {
            while (IsTurn)
            {
                Console.WriteLine(Run.WelcomeBattle());
                Console.WriteLine(PlayerInfo());
                Console.WriteLine("----> Battle Actions <----\n " +
                "1 - Attack!\n " +
                "2 - Scan!\n " +
                "3 - Switch Stance!\n " +
                "4 - Skills!\n " +
                "5 - Talk! (NOT IMPLEMENTED YET)\n " +
                "6 - Run! (NOT IMPLEMENTED YET)\n ");

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
                    
                    case 4:
                        if(BaseClass.ClassName(PlayerClass) == "Knight")
                        {
                            Knight knightVar = Knight.GetKnightBuffer();
                            knightVar.ListSkills();
                            knightVar.ChooseSkill(this,enemy);
                            Run.Wait();
                            Console.Clear();
                            IsTurn = false;
                            enemy.IsTurn = true;
                        }                 
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
            Console.Write("\nPlayer name: ");
            PlayerName = Console.ReadLine();
            SelectClass(this);
            //this.Class = selectedClass.GetType().Name == "Knight" ? PlayerClass as Knight : null;

            Console.Clear();
        }
        private double Attack(Enemy enemy)
        {
            var rnd = new Random();
            DMG = rnd.Next(5, 40);
            enemy.HP -= DMG; 
            Console.WriteLine($"{Name} attacks {enemy.Name} causing {DMG} DMG!");
            if (enemy.HP <= 0)
            {
                Console.WriteLine($"{Name} beat {enemy.Name}!");
                return 0;
            }
            return DMG;
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
                    this.DMG += 10;
                    Console.WriteLine($"{this.Name} changed stance to: {this.fightingStances}");
                    break;

                case 2:
                    this.fightingStances = (Stances)choice;
                    this.HP += 20;
                    Console.WriteLine($"{this.Name} changed stance to: {this.fightingStances}");
                    break;

                default:
                    Console.WriteLine("1 - 4 ONLY!");
                    SwitchStance();
                    break;
            }
        }
        private void Scan(Enemy enemy)
        {
            Console.WriteLine($"{Name} scans {enemy.Name}!");
            Console.WriteLine(enemy.EnemyInfo());
        }
        public void SelectClass(Player player)
        {
            Console.Clear();
            Console.WriteLine("-----> Please select a class <----- ");
            Console.WriteLine("1 - Knight (DMG/DEF)\n2 - Rogue (NOT IMPLEMENTED YET)");

            System.Console.Write("Choose class: ");
            var choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    System.Console.WriteLine($"{player.Name} is now a Knight!");
                    player.Class = new Knight();
                    Knight.OverridePlayerStats(this);
                    break;
                default:
                    System.Console.WriteLine("Not implemented.");
                    Run.Wait();
                    SelectClass(this);
                    break;
                   
            }
        }
        private string PlayerInfo()
        {
            return "\n - - - - - - - - - - - - - - - - - - -\n" +
            $"           CHARACTER INFO \n\nName: {PlayerName}\nClass: {this.Class.GetType().Name}\nHP: {HP:F2}"
            + "\n - - - - - - - - - - - - - - - - - - -\n";
        }
    }
}
