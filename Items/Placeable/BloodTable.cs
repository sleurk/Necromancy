using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Items.Placeable
{
	public class BloodTable : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Table");
            Tooltip.SetDefault("Right click to gain unholy blessings");
        }

        public override void SetDefaults()
		{
			item.width = 32;
			item.height = 34;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 3;
            item.createTile = mod.TileType("BloodTable");
		}

        public override void AddRecipes()
		{
            ModRecipe tableRecipe = new ModRecipe(mod);
            tableRecipe.AddIngredient(ItemID.Obsidian, 10);
            tableRecipe.AddIngredient(mod, "Brimstone", 10);
            tableRecipe.AddIngredient(mod, "BeatingHeart");
            tableRecipe.AddTile(TileID.Anvils);
            tableRecipe.SetResult(this);
            tableRecipe.AddRecipe();
		}
    }
}