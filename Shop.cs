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
            decimal potionPrice;
            decimal weaponPrice;
            decimal armorPrice;
            decimal difficultyPrice;
            while (true)
            {
                // 10% Discount for rogues
                potionPrice = Math.Round(5M * (p.difficultyMod + 1) * ((Program.currentPlayer.currentClass == Player.CharacterClass.Rogue) ? 0.9M : 0));
                weaponPrice = Math.Round(10M * p.weaponValue * ((Program.currentPlayer.currentClass == Player.CharacterClass.Rogue) ? 0.9M : 0));
                armorPrice = Math.Round(10M * (p.armorValue + 1) * ((Program.currentPlayer.currentClass == Player.CharacterClass.Rogue) ? 0.9M : 0));
                difficultyPrice = Math.Round(30M + 10M * p.difficultyMod * ((Program.currentPlayer.currentClass == Player.CharacterClass.Rogue) ? 2M : 0));
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
                else if (input == "quit" || input == "quit game" || input == "q")
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

        static void TryBuy(string item, decimal cost, Player p)
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
                Console.WriteLine("Thank you for your purchase.( " + item + " + 1)");
                p.gold -= Convert.ToInt32(Math.Round(cost));
            }
            else
            {
                Console.WriteLine("Sorry " + p.name + " I can't give credit. Come back when you're a little, mmm... richer.");
                GameOperations.PressAnyKeyToContinue();
            }
        }
    }
}
