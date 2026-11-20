using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualBasic;
using SharpBattle.Entities;

namespace SharpBattle.Util
{
    public class Utilities
    {
        public static void Wait(int seconds = 5)
        {
            for (var i = 0; i <= seconds; i++)
            {
                Console.Write(".");
                Thread.Sleep(seconds * 100);
            }
        }

        public static void CheckMissingDependencies()
        {
            List<string> missingDependencies = new();

            if (!IsSpectreConsoleAvailable()) 
            {
                missingDependencies.Add("Spectre.Console");
            }

            if (missingDependencies.Count > 0)
            {
                Console.WriteLine("The following dependencies are missing:");
                foreach (var dependency in missingDependencies)
                {
                    Console.WriteLine($"- {dependency}");
                }
                Console.WriteLine("Please install the missing dependencies and restart the game.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        private static bool IsSpectreConsoleAvailable()
        {
            try
            {
                // Attempt to use a Spectre.Console feature to check if it's available
                var test = new Spectre.Console.Markup("[green]Test[/]");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}