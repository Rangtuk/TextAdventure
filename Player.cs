namespace TextAdventure
{
    [Serializable]
    public class Player
    {
        public Random rand = new();

        public string name = "";
        public int playerID;

        public int level = 1;
        public int exp = 0;

        public int gold = 0;
        public int health = 10;
        public int potions = 5;
        public int armorValue = 0;
        public int weaponValue = 1;
        public int monstersKilled = 0;
        public int difficultyMod = 0;

        public enum CharacterClass { Mage, Cleric, Warrior, Rogue }
        public CharacterClass currentClass = CharacterClass.Warrior;

        public int GetXP()
        {
            int upper = 20 * difficultyMod + 50;
            int lower = 15 * difficultyMod + 10;
            return rand.Next(lower, upper);
        }

        public decimal GetLevelUpValue()
        {
            return (100M * level + 200M) * (Program.currentPlayer.currentClass == CharacterClass.Cleric ? 0.8M : 1);
            // 300, 400, 500 - Non Cleric
            // 240, 320, 400.. - Cleric
        }

        public bool CanLevelUp()
        {
            if (exp >= GetLevelUpValue())
                return true;
            return false;
        }

        public void LevelUp()
        {
            while (CanLevelUp())
            {
                exp -= Convert.ToInt32(GetLevelUpValue());
                level++;
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            GameOperations.Print("You have leveled up! You are now level " + level + "!");
            Console.ResetColor();
        }
    }
}
