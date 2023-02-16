using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace TextAdventure
{
    class Program
    {
        public static TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        public static Player currentPlayer = new();
        public static bool mainLoop = true;
        static void Main(string[] args)
        {
            if (!Directory.Exists("localSaves"))
            {
                Directory.CreateDirectory("localSaves");
            }
            currentPlayer = GameOperations.Load(out bool newP);
            if (newP)
            {
                GameOperations.Save();
                Encounters.FirstEncounter();
            }
            while (mainLoop)
                Encounters.RandomEncounterTable();
            // TEMP WIN SCREEN
            Console.Clear();
            Console.WriteLine("You win!");
            Console.WriteLine("You slew " + currentPlayer.monstersKilled + " monsters and ended with " + currentPlayer.gold + " gold.");
            Console.WriteLine("Armor was +" + currentPlayer.armorValue + " and your weapon was +" + currentPlayer.weaponValue + ".");
            Console.WriteLine("Difficulty was " + currentPlayer.difficultyMod + ".");
            Console.WriteLine("Conglaturations!");
            GameOperations.PressAnyKeyToContinue();
        }

        public static Player NewStart(int i)
        {
            Player p = new();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("TUTORIAL DUNGEON");
            p.name = GameOperations.PlayerInput("Name: ");
            p.playerID = i;
            Console.Clear();
            Console.WriteLine("You awaken in a dungeon. You feel dazed, and hurt all over. You cannot remember how you got here.");
            // no name
            if (p.name == "")
                Console.WriteLine("You can't even remember your name...");
            // normal name
            else
            {
                p.name = textInfo.ToTitleCase(p.name);
                Console.WriteLine("You remember your name is " + p.name + ".");
            }


            GameOperations.PressAnyKeyToContinue();

            Console.WriteLine("You fumble around as your eyes adjust to the dim lighting, you are in what appears to be a stone cell.");
            Console.WriteLine("An old door stands in front of you, with light peering through its cracks.");
            Console.WriteLine("You approach the door, opening it with ease. The mechanisms have long since rusted and fell apart.");

            GameOperations.PressAnyKeyToContinue();
            return p;
        }

    }
}