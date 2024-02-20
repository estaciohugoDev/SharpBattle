using System;
using System.Threading;
using SharpBattle.Entities;
using SharpBattle.Util;

namespace SharpBattle
{                  
    public static class GameStart
    {
        //TODO: Remake this structure. 
        //currently it just tosses the player into a battle
        public static void Start(Player player)
        {
            Console.Clear();
            Console.WriteLine("#--------- SHARPBATTLE ----------#");
            //Get enemy from list
            Enemy enemy = new();
            Utilities.TargetFound(player,enemy);

            //Starts all the fighting. 
            Utilities.FightLoop(player, enemy);
        }
    }
}