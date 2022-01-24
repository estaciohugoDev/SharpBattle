using System;
using System.Threading;
using SharpBattle.Entities;
using SharpBattle.Util;

namespace SharpBattle
{
    public static class Run
    {
        public static void Start(Player player)
        {
            Utilities.WelcomeBattle();

            //Get enemy from list
            player.EnemyFound();

            //Starts all the fighting. 
            Utilities.FightLoop(player, player.Enemy);
        }

        #region METHODS 
        #endregion METHODS
    }
}