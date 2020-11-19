using System;
using System.Threading;

namespace SharpBattle 
{
    public class Run
    {
        public Run() { }
        public void Start(Player.Player player)
        {
            Console.WriteLine(WelcomeBattle()); 
            //Create character, class system TBI

            player.CreatePlayer();
            var enemy = NewEnemy(); 

            //Get enemy from list
            EnemyFound(player, enemy);
            FightLoop(player, enemy); 
            System.Console.WriteLine("Test after fightloop");
        }

        private void FightLoop(Player.Player player, Enemy.Enemy enemy)
        {
            var x = 0;
            while (x == 0) //while(player.Health > 0 && enemy.Health > 0)
            {
                //Wait for player input
                if (player.Health > 0)
                    player.NextAction(enemy);
                else
                {
                    System.Console.WriteLine($"{player.Name} DIED! - GAME OVER");
                    break;
                }
                //Get randomized enemy action
                if (enemy.Health > 0)
                {
                    enemy.EnemyAction(player);
                }
                else
                {
                    System.Console.WriteLine("---- HERE COMES A NEW CHALLENGER ----");
                    player.IsTurn = true;
                    x = 1;
                    enemy = NewEnemy();
                    Console.WriteLine(EnemyFound(player, enemy));
                    FightLoop(player, enemy);
                }
            }
            System.Console.WriteLine("Fight over!");
            return;
        }
        private Enemy.Enemy NewEnemy()
        {
            var enemy = new Enemy.Enemy();

            return enemy;
        }
        public static string WelcomeBattle()
        {
            return "---------- SHARPBATTLE ----------";
        }
        private string EnemyFound(Player.Player player, Enemy.Enemy enemy)
        {
            return $"{player.Name} finds a {enemy.Name}, BATTLE START!";
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