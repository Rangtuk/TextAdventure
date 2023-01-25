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
        Weapon equippedWeapon = new Weapon();
        public int armorValue = 0;

        public struct Weapon
        {
            public int maxDamage;
            public int minDamage;

            public string quality;
            public string type;
            public int specialSlots;
            public string specialResourceName;
            public Weapon(string Type, string Quality, int Max, int Min, int SpecialSlots, string SpecialResourceName)
            {

                this.maxDamage = Max;
                this.minDamage = Min;

                this.quality = Quality;
                this.type = Type;
                this.specialSlots = SpecialSlots;
                this.specialResourceName = SpecialResourceName;
            }
        }

    }
}
