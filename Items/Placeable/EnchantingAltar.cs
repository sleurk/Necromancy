using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Items.Placeable
{
	public class EnchantingAltar : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("(WIP) Altar - Enchanting");
            Tooltip.SetDefault("Throw a weapon near the altar and use the altar with chalk to boost the weapon");
        }

        public override void SetDefaults()
		{
			item.width = 32;
			item.height = 16;
			item.maxStack = 99;
            item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.value = 0;
            item.rare = 3;
            item.createTile = mod.TileType("EnchantingAltar");
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.StoneBlock, 50);
            recipe.AddIngredient(ItemID.FallenStar, 10);
            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.SetResult(this);
            // recipe.AddRecipe();
        }
    }
}