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
        private static string FileName = $"{Player.PlayerName}.txt";
        private static FileInfo fi = new FileInfo(FileName);
        private static string projectPath = Directory.GetCurrentDirectory().ToString();
        private static string folderName = Path.Combine(projectPath, "SaveData");

        //<SAVE GAME>//
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
        //</SAVE GAME>//

        //<LOAD GAME>//
        //public static void LoadGame()
        //</LOAD GAME>//
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
                System.Console.WriteLine($"{counter + 1} - {files[counter].ToString()}");
                counter++;
            }
        }
        private static Boolean IsFolderCreated()
        {
            return Directory.Exists("SaveData") ? true : false;
        }
        private static Boolean CheckForFile()
        {
            return File.Exists(FileName) ? true : false;
        }

    }
}