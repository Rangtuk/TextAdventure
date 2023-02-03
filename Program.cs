namespace TextAdventure
{
    class Program
    {
        public static Player currentPlayer = new();
        public static bool mainLoop = true;
        static void Main(string[] args)
        {
            Start();
            Encounters.FirstEncounter();
            while (mainLoop)
                Encounters.RandomEncounter();

            // TEMP WIN SCREEN
            Console.Clear();
            Console.WriteLine("You win!");
            Console.WriteLine("You killed " + currentPlayer.monstersKilled + " monsters and ended with " + currentPlayer.gold + " gold.");
            Console.WriteLine("Conglaturations!");
            GameOperations.PressAnyKeyToContinue();
        }

        static void Start()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("TUTORIAL DUNGEON");
            currentPlayer.name = GameOperations.PlayerInput("Name: ");

            Console.Clear();
            Console.WriteLine("You awaken in a dungeon. You feel dazed, and hurt all over. You cannot remember how you got here.");
            if (currentPlayer.name == "")
                Console.WriteLine("You can't even remember your name...");
            else
                Console.WriteLine("You remember your name is " + currentPlayer.name + ".");

            GameOperations.PressAnyKeyToContinue();

            Console.WriteLine("You fumble around as your eyes adjust to the dim lighting, you are in what appears to be a stone cell.");
            Console.WriteLine("An old door stands in front of you, with light peering through its cracks.");
            Console.WriteLine("You approach the door, opening it with ease. The mechanisms have long since rusted and fell apart.");

            GameOperations.PressAnyKeyToContinue();
        }
    }
}