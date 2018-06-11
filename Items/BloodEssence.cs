using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items
{
	public class BloodEssence : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Essence");
        }

        public override void SetDefaults()
		{
			item.width = 20;
			item.height = 26;
			item.maxStack = 999;
			item.value = Item.sellPrice(0, 0, 2, 0);
			item.rare = 1;
        }

        public override void AddRecipes() // convert with thorium blood
        {
            Mod thorium = ModLoader.GetMod("ThoriumMod");
            if (thorium != null)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(this);
                recipe.SetResult(thorium, "Blood");
                recipe.AddRecipe();

                recipe = new ModRecipe(mod);
                recipe.AddIngredient(thorium, "Blood");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}