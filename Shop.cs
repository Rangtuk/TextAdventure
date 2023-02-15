using System;
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
                potionPrice = 2 * (p.difficultyMod + 1);
                weaponPrice = 10 * p.weaponValue;
                armorPrice = 10 * (p.armorValue + 1);
                difficultyPrice = 30 + 10 * p.difficultyMod;
            //label
            repeat:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Shop");
                Console.WriteLine("========================");
                Console.WriteLine("(P)otion:         $" + potionPrice + "   (Have: " + p.potions + ")");
                Console.WriteLine("(W)eapon:         $" + weaponPrice + "   (Currently: +" + p.weaponValue + ")");
                Console.WriteLine("(A)rmor:          $" + armorPrice + "   (Currently: +" + p.armorValue + ")");
                Console.WriteLine("(D)ifficulty Mod: $" + difficultyPrice + "   (Level: " + p.difficultyMod + ")");
                Console.WriteLine("========================");
                Console.WriteLine("Gold: " + p.gold + "     Health: " + p.health);
                Console.WriteLine("(E)xit Shop | (Q)uit Game");
                string input = GameOperations.PlayerInput("What do you want to buy? ");
                Console.Clear();
                if (input == "potion" || input == "p")
                {
                    TryBuy("potion", potionPrice, p);
                }
                else if (input == "weapon" || input == "w")
                {
                    TryBuy("weapon", weaponPrice, p);
                }
                else if (input == "armor" || input == "a")
                {
                    TryBuy("armor", armorPrice, p);
                }
                else if (input == "difficulty" || input == "difficulty mod" || input == "d")
                {
                    TryBuy("difficulty", difficultyPrice, p);
                }
                else if (input == "exit" || input == "exit game" || input == "e")
                {
                    Console.WriteLine("You leave the shop.");
                    break;
                }
                else if (input == "quit"|| input == "quit game" || input == "q")
                {
                    GameOperations.Quit();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(GameOperations.inputError);
                    goto repeat;
                }
            }

        }

        static void TryBuy(string item, int cost, Player p)
        {
            if (p.gold >= cost)
            {
                switch (item)
                {
                    case "potion":
                        p.potions++;
                        break;
                    case "weapon":
                        p.weaponValue++;
                        break;
                    case "armor":
                        p.armorValue++;
                        break;
                    case "difficulty":
                        p.difficultyMod++;
                        break;
                }
                Console.WriteLine("Thank you for your purchase.( " + item + " +1)");
                p.gold -= cost;
            }
            else
            {
                Console.WriteLine("Sorry " + p.name + " I can't give credit. Come back when you're a little, mmm... richer.");
                GameOperations.PressAnyKeyToContinue();
            }
        }
    }
}
