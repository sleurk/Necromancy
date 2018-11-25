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
            Tooltip.SetDefault("10% increased necrotic damage" +
                    "\n50% increased life steal" +
                    "\n100% increased blood purification rate");
        }

        public override void SetDefaults()
		{
			item.width = 36;
			item.height = 40;
            item.value = Item.sellPrice(0, 5);
            item.rare = 8;
			item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<NecromancyPlayer>(mod).necroticDamage += 0.10f;
            player.GetModPlayer<NecromancyPlayer>(mod).lifeStealMult += 0.5f;
            player.GetModPlayer<NecromancyPlayer>(mod).healCDLength -= 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AvengerEmblem);
            recipe.AddIngredient(mod, "SanguineContract");
            recipe.AddIngredient(mod, "Parchment");
            recipe.AddIngredient(ItemID.Ectoplasm, 10);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}