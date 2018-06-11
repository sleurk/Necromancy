using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class MidnightLeggings : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Midnight Leggings");
            Tooltip.SetDefault("18% increased movement speed" +
                "\n35% increased life regeneration");
        }

        public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
			item.value = Item.sellPrice(0, 1, 0, 0);
			item.rare = 8;
			item.defense = 11;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.18f;
            player.GetModPlayer<NecromancyPlayer>().regenMult += 0.35f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpookyWood, 125);
            recipe.AddIngredient(ItemID.Ectoplasm, 20);
            recipe.AddIngredient(ItemID.GraniteBlock, 30);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}