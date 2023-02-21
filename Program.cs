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
            GameOperations.Print("TUTORIAL DUNGEON", 15);
            p.name = GameOperations.PlayerInput("Name: ");

        // label
        repeat:
            bool flag = false;
            while (flag == false)
            {
                flag = true;
                GameOperations.Print("Warrior | Mage | Rogue | Cleric", 15);
                string input = GameOperations.PlayerInput("Class: ");
                if (input == "mage")
                    p.currentClass = Player.CharacterClass.Mage;
                else if (input == "cleric")
                    p.currentClass = Player.CharacterClass.Cleric;
                else if (input == "rogue")
                    p.currentClass = Player.CharacterClass.Rogue;
                else if (input == "warrior")
                    p.currentClass = Player.CharacterClass.Warrior;
                else
                {
                    flag = false;
                    Console.WriteLine("INVALID CLASS");
                    goto repeat;
                }
            }
            p.playerID = i;
            Console.Clear();
            GameOperations.Print("You awaken in a dungeon. You feel dazed, and hurt all over. You cannot remember how you got here.");
            // no name
            if (p.name == "")
                GameOperations.Print("You can't even remember your name...");
            // normal name
            else
            {
                p.name = textInfo.ToTitleCase(p.name);
                GameOperations.Print("You remember your name is " + p.name + ", a " + p.currentClass + ".");
            }


            GameOperations.PressAnyKeyToContinue();

            GameOperations.Print("You fumble around as your eyes adjust to the dim lighting, you are in what appears to be a stone cell.");
            GameOperations.Print("An old door stands in front of you, with light peering through its cracks.");
            GameOperations.Print("You approach the door, opening it with ease. The mechanisms have long since rusted and fallen apart.");

            GameOperations.PressAnyKeyToContinue();
            return p;
        }

    }
}