using System.Linq;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using SharpBattle.Entities;
using SharpBattle.Util;

namespace SharpBattle
{
    public static class SaveLoadFiles
    {
        private static readonly string FileName = $"{Player.PlayerName}.txt";
        private static readonly FileInfo fi = new FileInfo(FileName);
        private static readonly string projectPath = Directory.GetCurrentDirectory().ToString();
        private static readonly string folderName = Path.Combine(projectPath, "SaveData");

        #region SAVE GAME
        public static void SaveGame(Player playerData)
        {
            if (!IsFolderCreated())
                CreateFolder();

            if (!CheckForFile())
            {
                string fullFilePath = Path.Combine(folderName, FileName);
                try
                {
                    using (StreamWriter sw = new StreamWriter(fullFilePath))
                    {
                        foreach (string data in Player.CondensedPlayerInfo(playerData))
                        {
                            sw.WriteLine(data);
                        }
                    }
                    System.Console.WriteLine($"{playerData.Name} data has been saved!");
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
        public static Boolean SaveGamePrompt()
        {
            Boolean save = false;
            Console.Clear();
            System.Console.Write("-----> Would you like to save?\n----> Yes(Y).\n---> No.(N)\nChoice: ");
            string choice = Console.ReadLine().ToString().ToUpper();

            switch (choice)
            {
                case "Y":
                    save = true;
                    break;

                case "N":
                    save = false;
                    break;

                default:
                    Console.Clear();
                    System.Console.WriteLine("Error: Yes(Y) or No(N) values only!");
                    Utilities.Wait();
                    SaveGamePrompt();
                    break;
            }
            Console.Clear();
            return save;
        }
        #endregion SAVE GAME

        #region LOAD GAME 
        //public static void LoadGame()
        #endregion LOAD GAME 

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
                DirectoryInfo folder = new DirectoryInfo(folderName);
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
                Utilities.Wait();
                Utilities.MainMenuPrompt();
            }
        }
        private static Boolean IsFolderCreated()
        {
            return Directory.Exists("SaveData");
        }
        private static Boolean CheckForFile()
        {
            return File.Exists(FileName);
        }
        #endregion AUXILIARY METHODS

    }
}
