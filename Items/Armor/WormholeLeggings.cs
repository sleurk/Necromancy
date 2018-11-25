using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class WormholeLeggings : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wormhole Leggings");
            Tooltip.SetDefault("20% increased movement speed" +
                "\n25% increased life regeneration");
        }

        public override void SetDefaults()
		{
			item.width = 30;
			item.height = 18;
            item.value = 0;
			item.rare = 10;
			item.defense = 20;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.2f;
            player.GetModPlayer<NecromancyPlayer>().regenMult += 0.25f;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "FragmentWormhole", 15);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}