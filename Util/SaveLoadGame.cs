using System.Linq;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using SharpBattle.Entities;
using SharpBattle.Util;

namespace SharpBattle
{
    public static class SaveLoadGame
    {
        private static string FileName; //= $"{Player.Name}.txt";
        private static readonly FileInfo fi = new(FileName);
        private static readonly string projectPath = Directory.GetCurrentDirectory().ToString();
        private static readonly string folderName = Path.Combine(projectPath, "SaveData");

        public static void SaveGamePrompt(Player player)
        {
            Console.Clear();
            System.Console.Write("-----> Would you like to save?\n----> Yes(Y).\n---> No.(N)\nChoice: ");
            string choice = Console.ReadLine().ToUpper();

            switch (choice)
            {
                case "Y":
                    FileName = $"{player.Name}.txt";
                    SaveGame(player);
                    break;

                case "N":
                    break;

                default:
                    Console.Clear();
                    System.Console.WriteLine("Yes(Y) or No(N) values only!");
                    Utilities.Wait(3);
                    SaveGamePrompt(player);
                    break;
            }
            Console.Clear();
        }
        public static void SaveGame(Player playerData)
        {
            if (!IsFolderCreated())
                CreateFolder();

            if (!CheckForFile())
            {
                string fullFilePath = Path.Combine(folderName, FileName);
                try
                {
                    using (StreamWriter sw = new(fullFilePath))
                    {
                        foreach (var data in Player.PlayerInfo(playerData))
                        {
                            sw.WriteLine(data);
                        }
                    }
                    System.Console.WriteLine($"{playerData.Name} data has been saved!");
                    Utilities.Wait();
                }
                catch (Exception e)
                {
                    throw new Exception("Error: " + e.Message.ToString());
                }
            }
            else
            {
                fi.Delete();
                SaveGame(playerData);
            }
        }

        public static Player LoadGame()
        {
            //TODO: Show characters (filenames) and implement selection menu
            // read the contents of the file and instantiate a Player object.
            ShowCharacters();
            return default;
        }

        #region AUXILIARY METHODS
        public static void CreateFolder()
        {
            try
            {
                System.IO.Directory.CreateDirectory(folderName);
            }
            catch (Exception e)
            {
                throw new Exception("Error: " + e.Message.ToString());
            }
        }

        public static void ShowCharacters()
        {
            if(IsFolderCreated())
            {
                DirectoryInfo folder = new(folderName);
                string[] files = new string[folder.GetFiles().Length];
                int counter = 0;

                for (int i = 0; i < files.Length; i++)
                {
                    var fileHold = folder.GetFiles("*.txt").GetValue(i).ToString();
                    files[i] = Path.GetFileNameWithoutExtension(fileHold);
                }

                foreach (string file in files)
                {
                    System.Console.WriteLine($"{counter + 1} - {files[counter]}");
                    counter++;
                }
            }
            else
            {
                Console.Clear();
                System.Console.WriteLine("There are no save files, yet.");
                Utilities.Wait(3);
                Menus.MainMenuPrompt();
            }
        }
        private static Boolean IsFolderCreated(){return Directory.Exists("SaveData");}
        private static Boolean CheckForFile(){return File.Exists(FileName);}
        #endregion AUXILIARY METHODS

    }
}
