using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Items.Accessories
{
	public class SanguineContract : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sanguine Contract");
            Tooltip.SetDefault("+50% increased life steal");
        }

        public override void SetDefaults()
		{
			item.width = 36;
			item.height = 36;
            item.value = Item.sellPrice(0, 1);
            item.rare = 2;
			item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<NecromancyPlayer>(mod).lifeStealMult += 0.5f;
        }

        public override void AddRecipes()
        {
            BloodAlchemyRecipe recipe = new BloodAlchemyRecipe(mod);
            recipe.AddIngredient(mod, "Brimstone", 5);
            recipe.AddIngredient(mod, "BloodEssence", 20);
            recipe.AddIngredient(mod, "Parchment");
            recipe.AddTile(mod, "BloodAlchemyStation");
            recipe.SetResult(mod, "SanguineContract");
            recipe.AddRecipe();
        }
    }
}