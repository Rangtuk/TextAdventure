namespace TextAdventure
{
    [Serializable]
    public class Player
    {
        public string name = "";
        public int playerID;

        public int gold = 0;
        public int health = 10;
        public int potions = 5;
        public int armorValue = 0;
        public int weaponValue = 1;
        public int monstersKilled = 0;
        public int difficultyMod = 0;

        public enum CharacterClass {Mage, Cleric, Warrior, Rogue}
        public CharacterClass currentClass = CharacterClass.Warrior;
    }
}
