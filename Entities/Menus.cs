using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using SharpBattle.Util;

namespace SharpBattle.Entities
{
    public class Menus
    {
        public static void MainMenuPrompt()
        {
            Console.Clear();
            System.Console.WriteLine("----------- < SHARPBATTLE >\n" +
            "1 ------> New Game\n" +
            "2 ----> Load Game\n" +
            "3 ---> Quit\n\n");
            System.Console.Write("Choice: ");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    GameStart.Start(new Player());
                    break;
                case 2:
                    GameStart.Start(SaveLoadGame.LoadGame());
                    break;
                case 3:
                    System.Environment.Exit(0); //Kills game.
                    break;
                default:
                    MainMenuPrompt();
                    break;
            }
        }
        public static void GameOverScreen()
        {
            System.Console.WriteLine("-----> Would you like to continue?\n----> (Y)es.\n---> (N)o.\n");
            char choice = char.Parse(Console.ReadLine().ToUpper());
            Console.Clear();
            try
            {
                switch (choice)
                {
                    case 'Y':
                        MainMenuPrompt();
                        break;

                    case 'N':
                        return;

                    default:
                        System.Console.WriteLine("Please type (Y)es or (N)o ONLY!");
                        Utilities.Wait(3);
                        GameOverScreen();
                        break;
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Invalid input: {e.Message}");
                Utilities.Wait(3);
                GameOverScreen();
            }
        }
        public static int PlayerActions()
        {
            Console.WriteLine( 
            "----> Actions <----\n " +
                "1 - Attack!\n " +
                "2 - Scan!\n " +
                "3 - Switch Stance!\n " +
                "4 - Skills!\n ");
            Console.Write($"Choice: ");
            var ret = int.Parse(Console.ReadLine());
            return ret;
        }
    }
}