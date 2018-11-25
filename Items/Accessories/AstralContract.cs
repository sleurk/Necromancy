using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Accessories
{
	public class AstralContract : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astral Contract");
            Tooltip.SetDefault("Non-ranged life costs can be spent with mana at 15x the cost");
        }

        public override void SetDefaults()
		{
			item.width = 36;
			item.height = 40;
            item.value = Item.sellPrice(0, 1);
			item.rare = 2;
			item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<NecromancyPlayer>(mod).manaAcc = true;
        }

        public override void AddRecipes()
        {
            BloodAlchemyRecipe recipe = new BloodAlchemyRecipe(mod);
            recipe.AddIngredient(mod, "Brimstone", 5);
            recipe.AddIngredient(ItemID.FallenStar, 20);
            recipe.AddIngredient(mod, "Parchment");
            recipe.AddTile(mod, "BloodAlchemyStation");
            recipe.SetResult(mod, "AstralContract");
            recipe.AddRecipe();
        }
    }
}