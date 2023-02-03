namespace TextAdventure
{
    class Encounters
    {
        public static Random rand = new();
        // Encounters Generic

        // Encounters
        public static void FirstEncounter()
        {
            var enemyOrc = Monsters[/*ORC*/ 0];
            Console.Clear();
            if ("aeiou".Contains(enemyOrc.Name[0]))
                Console.WriteLine("Upon opening the door, you noticed an " + enemyOrc.Name + " with their back turned towards you, they have not noticed you yet.");
            else
                Console.WriteLine("Upon opening the door, you noticed a " + enemyOrc.Name + " with their back turned towards you, they have not noticed you yet.");
            Console.WriteLine("Outside the cell, a rusty shortsword sits propped up against the wall.");
            Console.WriteLine("You retrieve it and charge towards your former captor with newfound vigor.");
            Console.WriteLine("The " + enemyOrc.Name + " notices your charge, preparing to fight...");

            GameOperations.PressAnyKeyToContinue();

            Combat(enemyOrc.Name, enemyOrc.Power, enemyOrc.Health, enemyOrc.GoldMax);
        }
        public static void BasicFightEncounter()
        {
            Monster randomMonster = Monsters[rand.Next(0, Monsters.Count)];
        //LABEL
        repeat:
            if ("aeiou".Contains(randomMonster.Name))
                Console.WriteLine("As you turn the corner, you are confronted by an " + randomMonster.Name + "!");
            else
                Console.WriteLine("As you turn the corner, you are confronted by a " + randomMonster.Name + "!");
            Console.WriteLine("||  (F)ight  |  (R)un  |");
            string input = GameOperations.PlayerInput("You decide to... ");
            Console.Clear();
            if (input.ToLower() == "fight" || input.ToLower() == "f")
            {
                Console.WriteLine("You decide to fight!");
                Combat(randomMonster.Name, randomMonster.Power, randomMonster.Health, randomMonster.GoldMax);
            }
            else if (input.ToLower() == "run" || input.ToLower() == "r")
            {
                Console.WriteLine("You decide to run!");
                Console.WriteLine("You find a path around the " + randomMonster.Name + " before they have time to notice you.");
            }
            else
            {
                Console.WriteLine("INVALID ACTION");
                goto repeat;
            }
        }

        // Encounter Tools
        public static void RandomEncounter()
        {
            switch (rand.Next(0, 2))
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    BasicFightEncounter();
                    break;
                case 6:
                case 7:
                case 8:
                case 9:
                    //SHOP ENCOUNTER
                    break;
                case 10:
                    //RARE ENCOUNTER
                    break;
            }
        }
        public static void Combat(string name, int power, int health, int gold)
        {
            string mobName = name;
            int mobPower = power;
            int mobHealth = health;
            int mobGoldMax = gold;
            bool healFailed = false;
            bool isDefending = false;
            bool isFleeing = false;

            while (mobHealth > 0)
            {
            //LABEL
            repeat:
                Console.ForegroundColor = ConsoleColor.Yellow;
                if ("aeiou".Contains(mobName[0]))
                    Console.WriteLine("An " + mobName + " blocks your path.");
                else
                    Console.WriteLine("A " + mobName + " blocks your path.");
                Console.WriteLine("It has " + mobHealth + " health remaining.");
                Console.WriteLine("  Health: " + Program.currentPlayer.health + "   |   Potions: " + Program.currentPlayer.potions);
                Console.WriteLine("=================================");
                Console.WriteLine("||   (A)ttack  |  (D)efend     ||");
                Console.WriteLine("||    (H)eal   |   (F)lee      ||");
                Console.WriteLine("=================================");
                Console.ForegroundColor = ConsoleColor.White;
                string input = GameOperations.PlayerInput("You decide to... ");
                Console.Clear();
                int damageToMonster = rand.Next(1, 4) + rand.Next(1, Program.currentPlayer.weaponValue + 1);
                if (input.ToLower() == "attack" || input.ToLower() == "a")
                {
                    Console.WriteLine("You decide to attack.");
                    if (rand.Next(1, 21) == 20)
                    {
                        damageToMonster++;
                        damageToMonster *= 2;
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("CRITICAL HIT!!");
                        Console.WriteLine("You channel your strength into a mighty blow, you strike the " + mobName + " for " + damageToMonster + " damage!");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("You strike the " + mobName + " for " + damageToMonster + " damage.");
                    }
                    mobHealth -= damageToMonster;
                }
                else if (input.ToLower() == "defend" || input.ToLower() == "d")
                {
                    isDefending = true;
                    Console.WriteLine("You decide to defend.");
                    damageToMonster /= 2;
                    if (damageToMonster < 1)
                        damageToMonster = 0;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("You ready your weapon to parry the " + mobName + "'s attack. It attacks, and you riposte for " + damageToMonster + " damage.");
                    mobHealth -= damageToMonster;
                }
                else if (input.ToLower() == "heal" || input.ToLower() == "h")
                {
                    Console.WriteLine("You decide to heal.");
                    if (Program.currentPlayer.potions == 0)
                    {
                        healFailed = true;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("As you fumble around in your bags, all you can find are empty glass vials. You are out of potions.");
                    }
                    else
                    {
                        int potionValue = rand.Next(3, 7);
                        Console.WriteLine("You hastily retrieve a potion from your bags, uncorking it and swiftly downing the concoction.");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("You regain " + potionValue + " health.");
                        Program.currentPlayer.health += potionValue;
                        if (Program.currentPlayer.health > 10)
                        {
                            Program.currentPlayer.health = 10;
                            Console.WriteLine("You are at full health.");
                        }
                        Program.currentPlayer.potions -= 1;
                        GameOperations.PressAnyKeyToContinue();
                        continue;
                    }
                }
                else if (input.ToLower() == "flee" || input.ToLower() == "f")
                {
                    Console.WriteLine("You decide to flee.");

                    if (rand.Next(0, 2) == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("As you attempt to flee from the " + mobName + " it strikes! Catching you in the back, and halting your escape.");
                    }
                    else
                    {
                        isFleeing = true;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Success! You manage to evade the " + mobName + "'s blows. Sprinting past it and heading further into the dungeon.");
                        Console.ForegroundColor = ConsoleColor.White;
                        GameOperations.PressAnyKeyToContinue();
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("INVALID ACTION");
                    goto repeat;
                }
                if (mobHealth > 0)
                    Program.currentPlayer.health = MonsterTurn(isDefending, healFailed, mobPower, mobName, Program.currentPlayer.health);
                if (Program.currentPlayer.health <= 0)
                {
                    // U DIED LOL SO BAD HAHAHAHAHAHA
                    Console.Clear();
                    Console.WriteLine("The light beings to fade from your eyes you see the " + mobName + " stand over your body triumphantly...");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("YOU HAVE DIED");
                    Console.WriteLine("Press any key to pass on...");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                Console.ForegroundColor = ConsoleColor.White;
                GameOperations.PressAnyKeyToContinue();
            }
            if (isFleeing == false && mobHealth <= 0) // Victory
            {
                int goldGained = rand.Next(1, mobGoldMax + 1);
                Console.WriteLine("The " + mobName + " is slain! You gain " + goldGained + " gold.");
                Program.currentPlayer.gold += goldGained;
                Program.currentPlayer.monstersKilled += 1;
                GameOperations.PressAnyKeyToContinue();
            }
        }

        public static int MonsterTurn(bool isDefending, bool healFailed, int mobPower, string mobName, int playerHealth)
        {
            int damageToPlayer = mobPower - Program.currentPlayer.armorValue;
            if (isDefending == true)
                damageToPlayer /= 4;
            else if (healFailed == true)
                damageToPlayer /= 2;
            if (damageToPlayer < 1)
                damageToPlayer = 0;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The " + mobName + " strikes you for " + damageToPlayer + " damage.");
            return playerHealth -= damageToPlayer;
        }

        public static Dictionary<int, Monster> Monsters = new()
        {
            // Damage, Health, Gold
            {0, new("orc",2,5,7)},
            {1, new("ooze",1,6,5)},
            {2, new("goblin",1,3,10)}
        };
        public struct Monster
        {
            public int Power;
            public int Health;
            public int GoldMax;
            public string Name;
            public Monster(string name, int power, int health, int goldMax)
            {
                Name = name;
                Power = power;
                Health = health;
                GoldMax = goldMax;
            }
        }

    }
}
        
