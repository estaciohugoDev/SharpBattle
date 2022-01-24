using System;
using System.Threading;

namespace SharpBattle.Util
{
    public class Utilities
    {
        public static void WelcomeBattle()
        {
            Console.Clear();
            Console.WriteLine("#--------- SHARPBATTLE ----------#");
        }
        #region PROMPTS/SCREENS
        public static void GameOverScreen()
        {
            System.Console.WriteLine("-----> Would you like to continue?\n----> Yes(Y).\n---> No.(N)");
            char choice = char.Parse(Console.ReadLine());
            Console.Clear();

            try
            {
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
                        Wait();
                        GameOverScreen();
                        break;
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Invalid input: {e.Message}");
                Wait();
                GameOverScreen();
            }

        }
        public static void MainMenuPrompt()
        {
            Console.Clear();
            System.Console.WriteLine("----------- < MAIN MENU >");
            System.Console.WriteLine("1 ------ New Game");
            System.Console.WriteLine("2 ---- Load Game");
            System.Console.WriteLine("3 --- Quit");

            System.Console.Write("Choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Player player = new Player
                    {
                        Enemy = Enemy.NewEnemy()
                    };
                    Run.Start(player);
                    break;

                case 2:
                    System.Console.WriteLine("-------- < Select Character >");
                    SaveLoadFiles.ShowCharacters();
                    break;
            }
        }

        #endregion PROMPTS/SCREENS

        #region MAIN FIGHT LOOP
        public static void FightLoop(Player player, Enemy enemy)
        {
            var x = 0;
            while (x == 0)
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
                    Console.WriteLine(player.EnemyFound());
                    Console.WriteLine(enemy.EnemyInfo());
                    Utilities.Wait();
                    FightLoop(player, enemy);
                }
            }
            // return;
        }
        #endregion MAIN FIGHT LOOP
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