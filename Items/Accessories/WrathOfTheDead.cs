using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Accessories
{
	public class WrathOfTheDead : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wrath of the Dead");
            Tooltip.SetDefault("Necrotic summon max life costs do not increase with more minions" +
                "\nBase max life cost increased by 5");
        }

        public override void SetDefaults()
		{
			item.width = 36;
			item.height = 40;
            item.value = Item.sellPrice(0, 3, 20, 0);
			item.rare = 6;
			item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<NecromancyPlayer>(mod).summonAcc = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofMight);
            recipe.AddIngredient(ItemID.SoulofSight);
            recipe.AddIngredient(ItemID.SoulofFright);
            recipe.AddIngredient(mod, "Parchment");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}