using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class CursedHood : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cursed Hood");
            Tooltip.SetDefault("15% increased necrotic damage" +
                "\n-2 life cost");
        }

        public override void SetDefaults()
		{
			item.width = 20;
			item.height = 22;
			item.value = Item.sellPrice(0, 0, 75, 0);
			item.rare = 4;
			item.defense = 3;
		}

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<NecromancyPlayer>(mod).necroticMult += 0.15f;
            player.GetModPlayer<NecromancyPlayer>(mod).lifeCostModifier -= 2;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("CursedChestplate") && legs.type == mod.ItemType("CursedGreaves");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "+100 Max Life\nTaking damage temporarily increases necrotic damage";
            player.statLifeMax2 += 100;
            player.GetModPlayer<NecromancyPlayer>(mod).cursedSet = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CursedBar", 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}