using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Accessories
{
	public class RogueAgility : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rogue's Agility");
            Tooltip.SetDefault("Reduces chance to consume necrotic throwing weapons at low health");
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
            player.GetModPlayer<NecromancyPlayer>(mod).throwAcc = true;
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