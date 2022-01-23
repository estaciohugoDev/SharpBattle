using System;
using System.Threading;
using SharpBattle.Entities;
using SharpBattle.Util;

namespace SharpBattle
{
    public static class Run
    {
        public static void Start(Player player)
        {
            Console.WriteLine(Utilities.WelcomeBattle());

            var enemy = Enemy.NewEnemy();

            //Get enemy from list
            enemy.EnemyFound(player, enemy);
            FightLoop(player, enemy);
        }

        #region METHODS 
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
                    Utilities.GameOverScreen();
                    break;
                }
                //Get randomized enemy action
                if (enemy.HP > 0)
                    enemy.EnemyAction(player);

                else
                {
                    if (SaveLoadFiles.SaveGamePrompt())
                        SaveLoadFiles.SaveGame(player);

                    System.Console.WriteLine("---- HERE COMES A NEW CHALLENGER ----");
                    player.IsTurn = true;
                    x = 1;
                    enemy = Enemy.NewEnemy();
                    Console.WriteLine(enemy.EnemyFound(player, enemy));
                    Console.WriteLine(enemy.EnemyInfo());
                    Utilities.Wait();
                    FightLoop(player, enemy);
                }
            }
            return;
        }
        #endregion METHODS
    }
}