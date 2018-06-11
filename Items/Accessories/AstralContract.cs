using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Accessories
{
	public class AstralContract : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astral Contract");
            Tooltip.SetDefault("Non-ranged life costs can be spent with mana at 10x the cost");
        }

        public override void SetDefaults()
		{
			item.width = 36;
			item.height = 40;
            item.value = Item.sellPrice(0, 3, 20, 0);
			item.rare = 2;
			item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<NecromancyPlayer>(mod).manaAcc = true;
        }

        // recipe in BloodAlchemyStation.cs
    }
}