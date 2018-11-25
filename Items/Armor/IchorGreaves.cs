using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class IchorGreaves : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ichor Greaves");
            Tooltip.SetDefault("15% increased life regeneration");
        }

        public override void SetDefaults()
		{
			item.width = 30;
			item.height = 18;
            item.value = Item.sellPrice(0, 2);
            item.rare = 4;
			item.defense = 7;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.1f;
            player.GetModPlayer<NecromancyPlayer>().regenMult += 0.15f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "IchorBar", 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}