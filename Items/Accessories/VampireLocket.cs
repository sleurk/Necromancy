using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Accessories
{
	public class VampireLocket : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vampire Locket");
            Tooltip.SetDefault("Taking fatal damage will instead heal for all damage you take for one second" +
                "\n3 minute cooldown");
        }

        public override void SetDefaults()
		{
			item.width = 28;
			item.height = 28;
            item.value = Item.sellPrice(0, 3);
            item.rare = 6;
			item.accessory = true;
		}
         
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<NecromancyPlayer>(mod).vampireLocket = true;
        }
    }
}