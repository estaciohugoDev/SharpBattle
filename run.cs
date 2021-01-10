using System;
using System.Threading;
using SharpBattle.Entities;

namespace SharpBattle
{
    public static class Run
    {
        public static void Start(Player player)
        {
            Console.WriteLine(WelcomeBattle());
            //Create character, class system TBI

            //player.CreatePlayer();
            var enemy = Enemy.NewEnemy(); 

            //Get enemy from list
            enemy.EnemyFound(player, enemy);
            FightLoop(player, enemy); 
        }

        // ---------------- METHODS ---------------

        private static void FightLoop(Player player, Enemy enemy)
        {
            var x = 0;
            while (x == 0) //while(player.HP > 0 && enemy.HP > 0)
            {
                //Wait for player input
                if (player.HP > 0)
                    player.NextAction(enemy);
                else
                {
                    System.Console.WriteLine($"{player.Name} DIED! - GAME OVER");
                    GameOverScreen();
                    break;
                }
                //Get randomized enemy action
                if (enemy.HP > 0)
                    enemy.EnemyAction(player);
                
                else
                {
                    System.Console.WriteLine("---- HERE COMES A NEW CHALLENGER ----");
                    player.IsTurn = true;
                    x = 1;
                    enemy = Enemy.NewEnemy();
                    Console.WriteLine(enemy.EnemyFound(player, enemy));
                    FightLoop(player, enemy);
                }
            }
            return;
        }
        
        public static string WelcomeBattle()
        {
            Console.Clear();
            return "---------- SHARPBATTLE ----------";
        }
        public static void GameOverScreen()
        {
            System.Console.WriteLine("----- > Would you like to continue?\n----> Yes(Y).\n---> No.(N)");
            char choice = char.Parse(Console.ReadLine());
            Console.Clear();
            
            switch (choice)
            {
                case 'Y':
                    var player = new Player();
                    Run.Start(player);
                    break;

                case 'N':
                    return;
                
                default:
                    System.Console.WriteLine("Please type Y or N ONLY!");
                    GameOverScreen();
                    break;
            }
        }
        public static void Wait()
        {
            for (var i = 0; i <= 5; i++)
            {
                Console.Write(".");
                Thread.Sleep(500);
            }
        }
    }
}