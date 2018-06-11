using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class MidnightPlate : ModItem
	{
        public static float necroDamage = 0.1f;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Midnight Plate");
            Tooltip.SetDefault("25% increased necrotic critical strike chance");
        }

        public override void SetDefaults()
		{
			item.width = 34;
			item.height = 20;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 8;
			item.defense = 14;
		}

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<NecromancyPlayer>().necroticCritBonus += 25;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpookyWood, 150);
            recipe.AddIngredient(ItemID.Ectoplasm, 25);
            recipe.AddIngredient(ItemID.GraniteBlock, 40);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}