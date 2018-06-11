using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Items.Placeable
{
	public class ProtectionAltar : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Altar - Protection");
            Tooltip.SetDefault("Use the altar with chalk to negate knockback and increase defense");
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
			item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 3;
            item.createTile = mod.TileType("ProtectionAltar");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.StoneBlock, 50);
            recipe.AddIngredient(ItemID.Obsidian, 10);
            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}