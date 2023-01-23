namespace TextAdventure
{
    class Player
    {
        public string name;
        public int gold = 0;
        public int health = 10;
        public int maxDamage = 4;
        public int minDamage = 1;
        public int potion = 5;

        // Equipment
        public string weaponType; // Melee, Ranged
        public int weaponMaxValue = 0; //Attack value (not special)
        public int weaponMinValue = 0; /**/
        public int weaponSpecialSlots = 0;
        public int armorValue = 0;

        public struct Weapon
        {
            public string type;
            public int max;
            public int min;
            public int slots;
        }
        public Dictionary<string, Weapon> WeaponsDict =  new Dictionary<string, Weapon>() 
        {
            
        };
    }
}
