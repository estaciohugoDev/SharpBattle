using System;
using System.Threading;

namespace SharpBattle.Util
{
    public class Utilities
    {
        public static string WelcomeBattle()
        {
            Console.Clear();
            return "#--------- SHARPBATTLE ----------#";
        }
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
        public static void Wait()
        {
            for (var i = 0; i <= 5; i++)
            {
                Console.Write(".");
                Thread.Sleep(500);
            }
        }
        public static void MainMenuPrompt()
        {
            System.Console.WriteLine("----------- < MAIN MENU >");
            System.Console.WriteLine("1 ------ New Game");
            System.Console.WriteLine("2 ---- Load Game");
            System.Console.WriteLine("3 --- Quit");

            System.Console.Write("Choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Player player = new Player();
                    Run.Start(player);
                    break;

                case 2:
                    System.Console.WriteLine("-------- < Select Character >");
                    SaveLoadFiles.ShowCharacters();
                    break;
            }
        }
    }
}