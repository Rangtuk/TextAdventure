namespace TextAdventure
{
    class AdventureGame
    {
        public static Player currentPlayer = new();
        static void Main(string[] args)
        {
            Start();
            Encounters monster = new();
        }
        static void Start()
        {
            // Starting input
            Console.WriteLine("Adventure on High");
            Console.WriteLine("Name: ");
            currentPlayer.name = Console.ReadLine();
            // Story


            if (currentPlayer.name == "")
            {
                // No name input
                
            }
            else
            {

            }
            Console.ReadKey(); // Waiting to continue
            Console.Clear();
            // Story


        }
    }
}