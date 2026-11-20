using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using SharpBattle.Util;
using Spectre.Console;

namespace SharpBattle.Entities
{
    public class Menus
    {
        public static void MainMenuPrompt()
        {
            Console.Clear();

            var menuChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[cyan]Welcome to SharpBattle![/] "
                    + "\n[green]Please select an option:[/]")
                    .AddChoices
                    (
                        "New Game",
                        "Load Game",
                        "Quit"
                    ));

            switch (menuChoice)
            {
                case "New Game":
                    Game.Start(new Player());
                    break;
                case "Load Game":
                    Game.Start(SaveLoadGame.LoadGame());
                    break;
                case "Quit":
                    System.Environment.Exit(0); //Kills game.
                    break;
                default:
                    System.Console.WriteLine("Please enter a valid option!");
                    Utilities.Wait(3);
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