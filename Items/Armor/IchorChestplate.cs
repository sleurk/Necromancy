using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class IchorChestplate : ModItem
	{
        public static float necroDamage = 0.1f;
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ichor Chestplate");
            Tooltip.SetDefault("12% increased necrotic critical strike chance");
        }

        public override void SetDefaults()
		{
			item.width = 34;
			item.height = 22;
			item.value = Item.sellPrice(0, 0, 75, 0);
			item.rare = 4;
			item.defense = 10;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<NecromancyPlayer>().necroticCritBonus += 12;
        }

		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "IchorBar", 25);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}