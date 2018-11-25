using System;
using System.IO;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;

namespace Necromancy
{
    public static class Config
    {
        public static bool OtherEmpowerments = false;
        public static bool MoveEmpowerments = false;
        public static bool ExplicitlyShowNecroticSubtypes = false;

        // The file will be stored in "Terraria/ModLoader/Mod Configs/Necromancy.json"
        static readonly string ConfigPath = Path.Combine(Main.SavePath, "Mod Configs", "Necromancy.json");

        static Preferences Configuration = new Preferences(ConfigPath);

        public static void Load()
        {
            // Reading the config file
            bool success = ReadConfig();

            if (!success)
            {
                ErrorLogger.Log("Failed to read Necromancy's config file! Recreating config...");
                CreateConfig();
            }
        }

        // Returns "true" if the config file was found and successfully loaded.
        static bool ReadConfig()
        {
            if (Configuration.Load())
            {
                Configuration.Get("ShowOtherPlayerEmpowerments", ref OtherEmpowerments);
                Configuration.Get("MoveEmpowermentsToBottom", ref MoveEmpowerments);
                Configuration.Get("ExplicitlyShowNecroticSubtypes", ref ExplicitlyShowNecroticSubtypes);
                return true;
            }
            return false;
        }

        // Creates a config file. This will only be called if the config file doesn't exist yet or it's invalid. 
        static void CreateConfig()
        {
            Configuration.Clear();
            Configuration.Put("ShowOtherPlayerEmpowerments", OtherEmpowerments);
            Configuration.Put("MoveEmpowermentsToBottom", MoveEmpowerments);
            Configuration.Put("ExplicitlyShowNecroticSubtypes", ExplicitlyShowNecroticSubtypes);
            Configuration.Save();
        }
    }
}
