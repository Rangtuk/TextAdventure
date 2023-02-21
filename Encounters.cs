namespace TextAdventure
{
    class Encounters
    {
        public static Random rand = new();
        // Encounters Generic

        #region Encounters
        public static void FirstEncounter()
        {
            var enemyOrc = BasicMonsters[/*ORC*/ 0];
            Console.Clear();
            if ("aeiou".Contains(enemyOrc.Name[0]))
                GameOperations.Print("Upon opening the door, you noticed an " + enemyOrc.Name + " with their back turned towards you, they have not noticed you yet.");
            else
                GameOperations.Print("Upon opening the door, you noticed a " + enemyOrc.Name + " with their back turned towards you, they have not noticed you yet.");
            GameOperations.Print("Outside the cell, a rusty shortsword sits propped up against the wall.");
            GameOperations.Print("You retrieve it and charge towards your former captor with newfound vigor.");
            GameOperations.Print("The " + enemyOrc.Name + " notices your charge, preparing to fight...");

            GameOperations.PressAnyKeyToContinue();

            Combat(enemyOrc.Name, enemyOrc.Power, enemyOrc.Health, enemyOrc.GoldMax);
        }
        public static void BasicFightEncounter()
        {
            Monster randomMonster = BasicMonsters[rand.Next(0, BasicMonsters.Count)];
        //LABEL
        repeat:
            if ("aeiou".Contains(randomMonster.Name[0]))
                Console.WriteLine("As you turn the corner, you are confronted by an " + randomMonster.Name + "!");
            else
                Console.WriteLine("As you turn the corner, you are confronted by a " + randomMonster.Name + "!");
            Console.WriteLine("||  (F)ight  |  (R)un  ||");
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
                int chanceToRun = rand.Next(1, 101) + ((Program.currentPlayer.currentClass == Player.CharacterClass.Rogue) ? +5 : 0);
                if (chanceToRun <= 20) //20% to fail // 15% if Rogue
                {
                    Console.WriteLine("The " + randomMonster.Name + " notices your attempt to flee, and moves to block your escape! Seems you will have no choice but to fight.");
                    GameOperations.PressAnyKeyToContinue();
                    Combat(randomMonster.Name, randomMonster.Power, randomMonster.Health, randomMonster.GoldMax);
                }
                else
                {
                    Console.WriteLine("You find a path around the " + randomMonster.Name + " before they have time to notice you.");
                    GameOperations.PressAnyKeyToContinue();
                }
            }
            else
            {
                Console.WriteLine(GameOperations.inputError);
                goto repeat;
            }
        }
        public static void RareEncounter()
        {
            Monster randomMonster = RareMonsters[rand.Next(0, RareMonsters.Count)];
        //LABEL
        repeat:
            Console.WriteLine("As you turn the corner, you are confronted by " + randomMonster.Name + ", " + randomMonster.RareTitle + "!");
            Console.WriteLine("||  (F)ight  |  (R)un  ||");
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
                int chanceToRun = rand.Next(1, 101) + ((Program.currentPlayer.currentClass == Player.CharacterClass.Rogue) ? +5 : 0);
                if (chanceToRun <= 40) //40% to fail // 35% if Rogue
                {
                    Console.WriteLine(randomMonster.RareTitle + " notices your attempt to flee, and moves to block your escape! Seems you will have no choice but to fight.");
                    GameOperations.PressAnyKeyToContinue();
                    Combat(randomMonster.Name, randomMonster.Power, randomMonster.Health, randomMonster.GoldMax);
                }
                else
                {
                    Console.WriteLine(randomMonster.Name + " moves to intercept you. You charge forth with a burst of adrenaline, narrowly dodging " + randomMonster.RareTitle + "'s blows, and escaping further into the dungeon.");
                    GameOperations.PressAnyKeyToContinue();
                }
            }
            else
            {
                Console.WriteLine(GameOperations.inputError);
                goto repeat;
            }
        }
        public static void BossEncounter()
        {
            Console.WriteLine("Ahhh big spooky boss that doesnt exist yet! You win... probably");
        }
        public static void ShopEncounter()
        {
            Console.WriteLine("You come across a small shop.");
            Console.WriteLine("|| (E)nter       (L)eave ||");
            string input = GameOperations.PlayerInput("You decide to... ");
            if (input == "enter" || input == "e")
                Shop.LoadShop(Program.currentPlayer);
            else if (input == "leave" || input == "l")
            {
                System.Console.WriteLine("You leave the shop behind, heading further into the dungeon.");
                GameOperations.PressAnyKeyToContinue();
            }
        }
        public static void RandomEncounterTable()
        {
            int encounter = rand.Next(1, 101);
            if (encounter <= 55) // 55%
                BasicFightEncounter();
            else if (encounter <= 85) // 30%
                ShopEncounter();
            else if (encounter <= 95) // 10%
                RareEncounter();
            else if (encounter == 100) // 5%
                BossEncounter();
        }
        #endregion

        #region Encounter Tools
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
                Console.WriteLine("||    (H)eal   |   (R)un       ||");
                Console.WriteLine("=================================");
                Console.ForegroundColor = ConsoleColor.White;
                string input = GameOperations.PlayerInput("You decide to... ");
                Console.Clear();
                int damageToMonster = rand.Next(1, 4) + rand.Next(1, Program.currentPlayer.weaponValue + 1) + ((Program.currentPlayer.currentClass == Player.CharacterClass.Mage) ? +(Program.currentPlayer.weaponValue + 1) : 0);
                if (input == "attack" || input == "a")
                {
                    Console.WriteLine("You decide to attack.");
                    int critChance = rand.Next(1, 21);
                    if (critChance == 20 || (critChance >= 18 && Program.currentPlayer.currentClass == Player.CharacterClass.Warrior))
                    {
                        damageToMonster++;
                        damageToMonster *= 2;
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("CRITICAL HIT!!");
                        if (Program.currentPlayer.currentClass != Player.CharacterClass.Mage)
                            Console.WriteLine("You channel your strength into a mighty blow, you strike the " + mobName + " for " + damageToMonster + " damage!");
                        else
                            Console.WriteLine("You channel you mana into a powerful spell, you blast the " + mobName + " for " + damageToMonster + " damage!");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        if (Program.currentPlayer.currentClass != Player.CharacterClass.Mage)
                            Console.WriteLine("You strike the " + mobName + " for " + damageToMonster + " damage.");
                        else
                            Console.WriteLine("You blast the " + mobName + " with a spell for " + damageToMonster + " damage.");
                    }
                    mobHealth -= damageToMonster;
                }
                else if (input == "defend" || input == "d")
                {
                    isDefending = true;
                    Console.WriteLine("You decide to defend.");
                    damageToMonster /= 2;
                    if (damageToMonster < 1)
                        damageToMonster = 0;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    if (Program.currentPlayer.currentClass != Player.CharacterClass.Mage)
                        Console.WriteLine("You ready your weapon to parry the " + mobName + "'s attack. It attacks, and you riposte for " + damageToMonster + " damage.");
                    else
                        Console.WriteLine("You conjure a shield, absorbing part of the " + mobName + "'s attack and retailiate with a quick spell for " + damageToMonster + " damage.");
                    mobHealth -= damageToMonster;
                }
                else if (input == "heal" || input == "h")
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
                        int chanceForHit = rand.Next(0, 4);
                        int potionValue = rand.Next(2, 5) + ((Program.currentPlayer.currentClass == Player.CharacterClass.Cleric) ? +rand.Next(2, 5) : 0);
                        if (Program.currentPlayer.health < 10)
                        {
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
                            if (chanceForHit != 1) // 33%
                                continue;
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("The " + mobName + " takes a swing at you while you drink.");
                                healFailed = true;
                            }
                        }
                        else
                            Console.WriteLine("You are already at full health.");
                    }
                }
                else if (input == "run" || input == "r")
                {
                    Console.WriteLine("You decide to flee.");
                    int escapeChance = rand.Next(1, 101) + ((Program.currentPlayer.currentClass == Player.CharacterClass.Rogue) ? +2 : 0);
                    if (escapeChance <= 50) //50% base, 70% if Rogue
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
                    Console.WriteLine(GameOperations.inputError);
                    goto repeat;
                }

                if (mobHealth > 0)
                    Program.currentPlayer.health = MonsterTurn(isDefending, healFailed, mobPower, mobName, Program.currentPlayer.health);
                if (Program.currentPlayer.health <= 0)
                {
                    // U DIED LOL SO BAD HAHAHAHAHAHA
                    Console.Clear();
                    GameOperations.Print("The light beings to fade from your eyes you see the " + mobName + " stand over your body triumphantly...");
                    Console.ForegroundColor = ConsoleColor.Red;
                    GameOperations.Print("YOU HAVE DIED", 80);
                    Console.ForegroundColor = ConsoleColor.White;
                    GameOperations.Print("You killed " + Program.currentPlayer.monstersKilled + " monsters and ended with " + Program.currentPlayer.gold + " gold.");
                    GameOperations.Print("Press any key to pass on...");
                    Console.ReadKey();
                    GameOperations.Delete();
                    Environment.Exit(0);
                }
                Console.ForegroundColor = ConsoleColor.White;
                GameOperations.PressAnyKeyToContinue();
            }
            if (isFleeing == false && mobHealth <= 0) // Victory
            {
                int expGained = Program.currentPlayer.GetXP();
                int goldGained = rand.Next(1, mobGoldMax + 1);
                Console.WriteLine("The " + mobName + " is slain! You gain " + expGained + " experience and " + goldGained + " gold.");
                Console.WriteLine();
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

        public static Dictionary<int, Monster> BasicMonsters = new()
        {
            // Damage, Health, Gold
            {0, new("orc",2,5,11)},
            {1, new("ooze",1,6,9)},
            {2, new("goblin",1,3,14)},
        };
        public static Dictionary<int, Monster> RareMonsters = new()
        {
            // Damage, Health, Gold
            {0, new("Grumush Mansbane",3,17,25,"The Orc Chieftain")},
            {1, new("Fleshburner",2,20,23,"The Monstrous Ooze")},
            {2, new("Snikkspackle",2,13,30,"The Goblin King")},
        };
        public struct Monster
        {
            public int Power;
            public int Health;
            public int GoldMax;
            public string Name;
            public string RareTitle;
            public Monster(string name, int power, int health, int goldMax, string rareTitle = "")
            {
                Name = name;
                Power = power;
                Health = health;
                GoldMax = goldMax;
                RareTitle = rareTitle;
            }

        }
        #endregion
    }
}
