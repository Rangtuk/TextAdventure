namespace TextAdventure
{
    class Encounters
    {
        static Random rand = new Random();
        // Encounters
        #region
        public static void FirstEncounter()
        {
            // Story

            /* Wait to continue */Console.ReadKey();
        }
        #endregion
        // Encounter Tools
        #region
        public static void Combat(bool random, string name, int powerMin, int powerMax, int health, Player.Weapon weapon)
        {
            string monName = "";
            int monPowerMin = 0;
            int monPowerMax = 0;
            int monHP = 0;
            int distance = 0;
            string[] attackActions = { "(A)ttack", "(A)dvance" };
            string[] specialActions = { "(S)pecial", "(W)ait", "(E)vade" };

            if (random)
            { 
            
            }
            else
            {
                monName = name;
                monHP = health;
                monPowerMin = powerMin;
                monPowerMax = powerMax;
            }
            while (monHP < 0)
            {
                bool outOfRange = false;
                bool defending = false;
                Console.WriteLine("===================================");
                if (distance > 0 && weapon.type == "Melee") // Monster too far
                {
                    Console.WriteLine($"  {attackActions[1]}  |  {specialActions[1]}  "); // Advance | Wait
                    outOfRange = true;
                }
                else if (distance <= 0 && weapon.type == "Ranged") // Monster too close
                    Console.WriteLine($"  {attackActions[0]}  |  {specialActions[2]}  "); // Attack | Evade
                else // Monster in range
                    Console.WriteLine($"  {attackActions[0]}  |  {specialActions[0]}  "); // Attack | Special
                Console.WriteLine("|  (D)efend  |  (F)lee     |");
                Console.WriteLine("===================================");
                Console.WriteLine("Health: " + AdventureGame.currentPlayer.health + "   Potions: " + AdventureGame.currentPlayer.potion);
                string input = Console.ReadLine();
                Console.Clear();
                // Player Actions
                if (input.ToLower() == "attack" || input.ToLower() == "a" && outOfRange == false)
                {
                    // Attack
                }
                else if (input.ToLower() == "advance" || input.ToLower() == "a" && outOfRange == true)
                {
                    // Advance
                }
                else if (input.ToLower() == "special" || input.ToLower() == "s")
                {
                    // Weapon Special
                }
                else if (input.ToLower() == "wait" || input.ToLower() == "w" && outOfRange == true)
                {
                    // Wait for monster to advance
                }
                else if (input.ToLower() == "defend" || input.ToLower() == "d")
                {
                    // Defend
                }
                else if (input.ToLower() == "flee" || input.ToLower() == "f")
                {
                    // Escape encounter
                }
                else
                {
                    // Invalid Input
                    Console.WriteLine("Invalid action");
                    continue;
                }
            }
        }

        public static void OldSystem()
        {
            /*
            #region Old Combat System
            // TEMP //
            var playerHealth = 20;
            var mobHealth = 20;
            var spellSlots = 3;
            // TEMP //
            while (playerHealth > 0 && mobHealth > 0)
            {
                var blocking = false;
                Console.WriteLine("You have " + playerHealth + " health and " + spellSlots + " spellslots remaining.");
                Console.WriteLine("The monster has " + mobHealth + " health.");
                Console.WriteLine("You can:");
                Console.WriteLine("attack | defend | use magic | use item");
                var option = Console.ReadLine().ToLower();
                Console.Clear();
                // process player action
                if (option == "attack")
                {
                    // if player attacks
                    mobHealth = PlayerAttack(mobHealth);
                }
                else if (option == "defend")
                {
                    // if player defends
                    Console.WriteLine("You ready yourself against the monster.");
                    blocking = true;
                }
                else if (option == "use magic")
                {
                    // if player uses magic
                    if (spellSlots != 0)
                        playerMagic(ref spellSlots, ref mobHealth, ref playerHealth);
                    else
                    {
                        Console.WriteLine("You are out of spell slots.");
                        continue;
                    }
                }
                else if (option == "use item")
                {
                    // if player uses an item
                    Console.WriteLine("You have zero items left.");
                    continue;
                }
                else
                {
                    Console.WriteLine("Unrecognized action.");
                    continue;
                }
                if (mobHealth > 0)
                    playerHealth = MobAttack(playerHealth, blocking);

            };

            Console.Clear();
            if (playerHealth <= 0)
            {
                Console.WriteLine("You have died.");
            }
            if (mobHealth <= 0)
            {
                Console.WriteLine("You have slain the monster!");
            }

            static int PlayerAttack(int mobHP)
            {
                var rndDmg = new Random();
                var rndHit = new Random();
                var dmg = rndDmg.Next(1, 4);
                var toHit = rndHit.Next(1, 21);
                if (toHit == 20)
                {
                    mobHP -= dmg * 2;
                    Console.WriteLine("CRITICAL HIT! You hit for " + ((dmg + 1) * 2) + " damage!");
                }
                else if (toHit == 1)
                    Console.WriteLine("You miss!");
                else
                {
                    Console.WriteLine("You hit for " + dmg + " damage.");
                    mobHP -= dmg;
                }
                return mobHP;
            }

            static int MobAttack(int playerHP, bool blocking)
            {
                var rndDmg = new Random();
                var rndHit = new Random();
                var dmg = rndDmg.Next(1, 4);
                var toHit = rndHit.Next(1, 21);
                if (blocking == false)
                {
                    if (toHit <= 3)
                        Console.WriteLine("The monster attacks, but misses!");
                    else
                    {
                        Console.WriteLine("The monster hits you for " + dmg + " damage.");
                        playerHP -= dmg;
                    }
                }
                else
                {
                    if (toHit <= 3)
                        Console.WriteLine("The monster attacks, but misses!");
                    else
                    {
                        Console.WriteLine("You block the attack! The monster would have hit you for " + dmg + " damage.");
                    }
                }
                return playerHP;
            }

            static void playerMagic(ref int slots, ref int mobHP, ref int playerHP)
            {
                Console.WriteLine("What would you like to cast?");
                Console.WriteLine("Spells known: firebolt | heal");
                var rnd = new Random();
                var spell = Console.ReadLine().ToLower();
                Console.Clear();
                if (spell == "firebolt")
                {
                    var spellDmg = rnd.Next(2, 5);
                    mobHP -= spellDmg;
                    Console.WriteLine("A bolt of fire blasts the monster for " + spellDmg + " damage.");
                }
                if (spell == "heal")
                {
                    var spellHeal = rnd.Next(3, 5);
                    playerHP += spellHeal;
                    Console.WriteLine("The spell heals you for " + spellHeal + " health.");
                    if (playerHP > 20)
                        playerHP = 20;
                }
                slots--;
            }
            #endregion
            */
        }
        #endregion
    }
}
        
