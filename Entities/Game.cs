using System;
using System.Threading;
using SharpBattle.Entities;
using SharpBattle.Util;

namespace SharpBattle
{
    public static class Game
    {
        public static bool loop = true;

        //TODO: Remake this structure. 
        //currently it just tosses the player into a battle
        public static void Start(Player player)
        {
            Console.Clear();
            Console.WriteLine("#--------- SHARPBATTLE ----------#");
            //Get enemy from list
            Enemy enemy = new();
            Player.TargetFound(player, enemy);

            //Starts all the fighting. 
            FightLoop(player, enemy);
        }

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
                    Player.TargetFound(player, enemy);
                    Console.WriteLine(enemy.EntityInfo());
                    Utilities.Wait();
                    FightLoop(player, enemy);
                }
            }
            // return;
        }

    }
}