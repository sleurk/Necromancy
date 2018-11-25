using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class ApocalypseBreastplate : ModItem
	{
        public static float necroDamage = 0.1f;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Apocalypse Breastplate");
            Tooltip.SetDefault("20% increased necrotic critical strike chance");
        }
        
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 20;
            item.value = Item.sellPrice(0, 2);
            item.rare = 7;
			item.defense = 14;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<NecromancyPlayer>().necroticCrit += 20;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 24);
            recipe.AddIngredient(mod, "LivingHeart", 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}