using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Accessories
{
    [AutoloadEquip(EquipType.Shield)]
    public class CelestialShield : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Shield");
            Tooltip.SetDefault("-20% max life" +
                             "\n+30 defense after being hit");
        }

        public override void SetDefaults()
		{
			item.width = 26;
			item.height = 28;
			item.value = Item.sellPrice(0, 2, 0, 0);
			item.rare = 5;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
            player.GetModPlayer<NecromancyPlayer>(mod).celestialAccessory = true;
            player.statLifeMax2 -= (int)(player.statLifeMax * 0.2f);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CelestialBar", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}