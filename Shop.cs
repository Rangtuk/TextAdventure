namespace TextAdventure
{
    public class Shop
    {
        public static void LoadShop(Player p)
        {
            RunShop(p);
        }

        public static void RunShop(Player p)
        {
            int potionPrice;
            int weaponPrice;
            int armorPrice;
            int difficultyPrice;
            while (true)
            {
                //label
                refresh:
                potionPrice = 2 + 1 * p.difficultyMod;
                weaponPrice = 10 * (p.weaponValue + 1);
                armorPrice = 10 * p.armorValue;
                difficultyPrice = 30 + 10 * p.difficultyMod;
                Console.WriteLine("Shop           Gold: " + Program.currentPlayer.gold);
                Console.WriteLine("========================");
                Console.WriteLine("(P)otion:         $" + potionPrice);
                Console.WriteLine("(W)eapon:         $" + weaponPrice);
                Console.WriteLine("(A)rmor:          $" + armorPrice);
                Console.WriteLine("(D)ifficulty Mod: $" + difficultyPrice);
                Console.WriteLine("========================");
                string input = GameOperations.PlayerInput("What do you want to buy? ");
                if (input == "potion" || input == "p")
                {
                    // Potion buy
                }
                else if (input == "weapon" || input == "w")
                {
                    // weapon buy
                }
                else if (input == "armor" || input == "a")
                {
                    //armor buy
                }
                else if (input == "difficulty" || input == "d")
                {
                    // diff buy
                }
                else
                {
                    Console.WriteLine("INVALID INPUT");
                    goto refresh;
                }
            }

        }
    }
}
