using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Items
{
    public class ThoriumRecipe : ModRecipe
    {
        // Custom recipe type
        public ThoriumRecipe(Mod mod) : base(mod)
        {
        }

        // The only thing this does that's different from a normal recipe is that it disables the recipe without Thorium installed.
        // I don't want to have problems with stealing Thorium's radiant and symphonic content, so it is disabled if Thorium is not also installed
        public override bool RecipeAvailable()
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
    }
}