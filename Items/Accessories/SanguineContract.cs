using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Accessories
{
	public class SanguineContract : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sanguine Contract");
            Tooltip.SetDefault("Halves your life regen" +
                    "\n20% increased life steal");
        }

        public override void SetDefaults()
		{
			item.width = 36;
			item.height = 36;
			item.value = Item.sellPrice(0, 0, 50, 0);
			item.rare = 2;
			item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<NecromancyPlayer>(mod).regenMult *= 0.5f;
            player.GetModPlayer<NecromancyPlayer>(mod).lifeStealMult += 0.2f;
        }

        // recipe in BloodAlchemyStation.cs
    }
}