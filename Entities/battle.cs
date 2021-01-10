using SharpBattle.Entities;

namespace SharpBattle
{
    class Battle
    {
        public static void Main(string[] args)
        {
            var player = new Player();
            //Start the running class. 
            Run.Start(player);
        }
    }
}
