using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Accessories
{
	public class EldritchScroll : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eldritch Scroll");
            Tooltip.SetDefault("15% increased necrotic damage" +
                    "\n-2 necrotic life cost" +
                    "\n10% increased life steal");
        }

        public override void SetDefaults()
		{
			item.width = 36;
			item.height = 36;
            item.value = Item.sellPrice(0, 3, 20, 0);
			item.rare = 8;
			item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<NecromancyPlayer>(mod).necroticMult *= 1.15f;
            player.GetModPlayer<NecromancyPlayer>(mod).lifeCostModifier -= 2;
            player.GetModPlayer<NecromancyPlayer>(mod).lifeStealMult += 0.1f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "NecromancerEmblem");
            recipe.AddIngredient(mod, "SanguineContract");
            recipe.AddIngredient(mod, "Parchment");
            recipe.AddIngredient(ItemID.SpookyWood, 200);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}