using SharpBattle.Entities;

namespace SharpBattle
{
    class Battle
    {
        public static void Main(string[] args)
        {
            var player = new Player();
            //Instance running class
            var runProgram = new Run();

            //Start the running class. 
            runProgram.Start(player);
        }
    }
}
