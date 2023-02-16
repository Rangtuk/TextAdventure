using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace TextAdventure
{
    class GameOperations
    {
        public const string inputError = "UNRECOGNIZED INPUT";
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

        public static void Quit()
        {
            Save();
            Environment.Exit(0);
        }
        
        public static void Save()
        {
            BinaryFormatter binForm = new();
            string path = "localSaves/" + Program.currentPlayer.playerID.ToString();
            FileStream file = File.Open(path, FileMode.OpenOrCreate);
            binForm.Serialize(file, Program.currentPlayer);
            file.Close();
        }

        public static void Delete()
        {
            BinaryFormatter binForm = new();
            string path = "localSaves/" + Program.currentPlayer.playerID.ToString();
            File.Delete(path);
        }

        public static Player Load(out bool newP)
        {
            newP = false;
            Console.Clear();
            string[] paths = Directory.GetFiles("localSaves");
            List<Player> players = new();
            int idCount = 0;

            BinaryFormatter binForm = new();
            foreach (string path in paths)
            {
                FileStream file = File.Open(path, FileMode.Open);
                Player player = (Player)binForm.Deserialize(file);
                file.Close();
                players.Add(player);
            }

            idCount = players.Count;

            while (true)
            {
                Console.Clear();
                System.Console.WriteLine("Select save: ");

                foreach (Player player in players)
                {
                    Console.WriteLine(player.playerID + ": " + player.name);
                }

                Console.WriteLine("Enter character name or id (id:# or playername)");
                Console.WriteLine("Alternatively 'create' will make a new save");
                string[] data = Console.ReadLine().Split(':');
                try
                {
                    if (data[0] == "id")
                    {
                        if (int.TryParse(data[1], out int id))
                        {
                            foreach (Player player in players)
                            {
                                if (player.playerID == id)
                                    return player;
                            }
                            Console.WriteLine("Character Id not found!");
                            GameOperations.PressAnyKeyToContinue();
                        }
                        else
                        {
                            System.Console.WriteLine("Invalid input: Id needs to be a number!");
                            GameOperations.PressAnyKeyToContinue();
                        }
                    }
                    else if (data[0] == "create")
                    {
                        Console.Clear();
                        Player newPlayer = Program.NewStart(idCount);
                        newP = true;
                        return newPlayer;
                    }
                    else
                    {
                        foreach (Player player in players)
                        {
                            if (player.name == Program.textInfo.ToTitleCase(data[0]))
                            {
                                Console.Clear();
                                return player;
                            }
                        }
                        Console.WriteLine("Character name not found!");
                        GameOperations.PressAnyKeyToContinue();
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    System.Console.WriteLine("Invalid input: Id needs to be a number!");
                    GameOperations.PressAnyKeyToContinue();
                }
            }
        }
    }
}
