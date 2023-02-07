namespace TextAdventure
{
    public class Shop
    {
        static int armorMod;
        static int weaponMod;
        static int diffMod;

        public static void LoadShop(Player p)
        {
            armorMod = p.armorValue;
            weaponMod = p.weaponValue;
            diffMod = p.difficultyMod;

            RunShop(p);
        }

        public static void RunShop(Player p)
        {
            Console.WriteLine("I'm Pumat Sol, how are you. I am also Pumat Sol.");
        }
    }
}
