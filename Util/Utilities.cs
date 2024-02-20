using System;
using System.Threading;
using Microsoft.VisualBasic;
using SharpBattle.Entities;

namespace SharpBattle.Util
{
    public class Utilities
    {
        public static bool loop = true;

        #region MAIN FIGHT LOOP
        public static void FightLoop(Player player, Enemy enemy)
        {
            while (loop)
            {
                //Wait for player input
                if (player.HP > 0)
                    player.Actions(enemy);
                else
                {
                    System.Console.WriteLine($"{player.Name} DIED! - GAME OVER");
                    Menus.GameOverScreen();
                    break;
                }
                //Get randomized enemy action
                if (enemy.HP > 0)
                    enemy.Actions(player);
                else
                {
                    Console.WriteLine("---- HERE COMES A NEW CHALLENGER ----");
                    player.IsTurn = true;
                    enemy = Enemy.NewEnemy();
                    TargetFound(player,enemy);
                    Console.WriteLine(enemy.EnemyInfo());
                    Wait();
                    FightLoop(player, enemy);
                }
            }
            // return;
        }
        #endregion MAIN FIGHT LOOP
        public static void Wait(int seconds = 3)
        {
            for (var i = 0; i <= seconds; i++)
            {
                Console.Write(".");
                Thread.Sleep(seconds * 100);
            }
        }
        public static void TargetFound(Player player ,BaseEntity target)
        {
            Console.WriteLine($"{player.Name} finds a {target.Name}.");
        }
    }
}