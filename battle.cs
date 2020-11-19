namespace SharpBattle.Battle
{
    class Battle
    {
        public static void Main(string[] args)
        {
            var player = new Player.Player();
            //Instance running class
            var runProgram = new Run();

            //Start the running class. 
            runProgram.Start(player);
        }
    }
}
