using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Accessories
{
	public class MuseMind : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Muse's Mind");
            Tooltip.SetDefault("Refreshes all necrotic empowerments when a buff is reapplied" +
                "\nEmpowerment time reduced to 5 seconds");
        }

        public override void SetDefaults()
		{
			item.width = 36;
			item.height = 40;
            item.value = Item.sellPrice(0, 3);
            item.rare = 6;
			item.accessory = true;
		}
         
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<NecromancyPlayer>(mod).symphAcc = true;
            player.GetModPlayer<NecromancyPlayer>(mod).empowermentMaxTime = 300; // 5 seconds
        }

        public override void AddRecipes()
        {
            ThoriumRecipe recipe = new ThoriumRecipe(mod);
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