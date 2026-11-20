using System;
using SharpBattle.Entities.Enums;
using System.Collections.Generic;
using SharpBattle.Entities;
using SharpBattle.Util;
using Spectre.Console;

namespace SharpBattle
{
    public class Player : BaseEntity
    {
        public bool IsTurn = true;
        public Player() { CreatePlayer(); }

        private void PlayerActions(Enemy target)
        {
            //TODO: 1) Implement option to Run from battle.
            //      2) Implement option to talk to targets.  

            var action = Menus.PlayerActions();
            switch (action)
            {
                case 1:
                    Attack(target);
                    Utilities.Wait();
                    Console.Clear();
                    IsTurn = false;
                    target.IsTurn = true;
                    break;
                case 2:
                    Scan(target);
                    Utilities.Wait();
                    Console.Clear();
                    IsTurn = false;
                    target.IsTurn = true;
                    break;
                case 3:
                    ChooseStance();
                    Utilities.Wait();
                    Console.Clear();
                    IsTurn = false;
                    target.IsTurn = true;
                    break;
                case 4:
                    if (BaseClass.ClassName(Class) == "Knight")
                    {
                        Knight knightVar = (Knight)Class;
                        knightVar.ListSkills();
                        Knight.ChooseSkill(this, target);
                        Utilities.Wait();
                        Console.Clear();
                        IsTurn = false;
                        target.IsTurn = true;
                    }
                    break;
                default:
                    Console.WriteLine("Values between 1 - 4 ONLY!");
                    Utilities.Wait();
                    Console.Clear();
                    PlayerActions(target);
                    break;
            }
        }
        public void Actions(Enemy target)
        {
            while (IsTurn)
            {
                Console.WriteLine(PlayerSheet());
                PlayerActions(target);
            }
        }
        private Player CreatePlayer()
        {
            try
            {
                Name = AnsiConsole.Ask<string>("\nPlayer [green]name: [/]");
                AnsiConsole.MarkupLine($"Welcome, [blue]{Name}[/]!");
                Utilities.Wait(5);
                BaseClass.SelectClass(this);
                return default;
            }
            catch (FormatException e)
            {
                AnsiConsole.MarkupLine($"Invalid input: [red]{e.Message}[/]");
                Utilities.Wait();
                CreatePlayer();
                return default;
            }
        }
        private void ChooseStance()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine(
                "Choose one of the following: \n" +
                "1 - Neutral \n" +
                "2 - Attacking (+STR) \n" +
                "3 - Defending (+DEF) \n");
            int choice = AnsiConsole.Ask<int>("Switching to: ");

            try
            {
                SwitchStance((FightingStances)choice);
                AnsiConsole.MarkupLine($"[green]{Name} changed stance to: {Stance}[/]");

                if(choice < 1 || choice > 3)
                {
                    AnsiConsole.MarkupLine($"[red]Please enter a value between 1 and 3.[/]");
                    Utilities.Wait();
                    ChooseStance();

                }
            }
            catch (FormatException e)
            {
                AnsiConsole.MarkupLine($"[red]Invalid input: {e.Message}[/]");
                Utilities.Wait();
                ChooseStance();
            }
        }

        private void Scan(BaseEntity target)
        {
            Console.Clear();
            AnsiConsole.MarkupLine($"[green]{Name}[/] scans [red]{target.Name}![/]");
            AnsiConsole.MarkupLine(target.EntityInfo());
        }

        public static void TargetFound(Player player, BaseEntity target)
        {
            AnsiConsole.MarkupLine($"[green]{player.Name}[/] finds a [red]{target.Name}![/]");
        }

        public static List<string> PlayerInfo(Player player)
        {
            return
            [
                player.Name,
                player.Class.GetType().Name,
                player.HP.ToString(),
                player.DEF.ToString(),
                player.MAG.ToString(),
                player.HLY.ToString(),
                player.LCK.ToString()
            ];
        }
        private string PlayerSheet()
        {
            return "\n - - - - - - - - - - - - - - - - - - -\n" +
            $"           CHARACTER INFO \n\nName: {Name}\nClass: {Class.Name}\nHP: {HP:F2}\nStance: {Stance.ToString()}\n"
            + "\n - - - - - - - - - - - - - - - - - - -\n";
        }
    }
}
