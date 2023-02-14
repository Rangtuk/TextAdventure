namespace TextAdventure
{
    class GameOperations
    {
        public static string PlayerInput(string value)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(value);
            return Console.ReadLine().Trim().ToLower();
        }

        public static void PressAnyKeyToContinue()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
