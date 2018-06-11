using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class IchorHood : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ichor Hood");
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
			return body.type == mod.ItemType("IchorChestplate") && legs.type == mod.ItemType("IchorGreaves");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "+100 Max Life\nDealing necrotic damage increases defense by 20 for 3 seconds";
            player.statLifeMax2 += 100;
            player.GetModPlayer<NecromancyPlayer>(mod).ichorSet = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "IchorBar", 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}