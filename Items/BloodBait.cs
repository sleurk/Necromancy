using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items
{
	public class BloodBait : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Bait");
            Tooltip.SetDefault("For the lazy.");
        }

        public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			item.maxStack = 999;
			item.value = Item.sellPrice(0, 0, 2, 0);
			item.rare = 2;
            item.bait = 10;
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "BeatingHeart");
            recipe.AddIngredient(mod, "BloodEssence", 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
	}
}