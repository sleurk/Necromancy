using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Accessories
{
	public class WraithCloak : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wraith Cloak");
            Tooltip.SetDefault("Double tap up and hold to fly towards your cursor for some life");
        }

        public override void SetDefaults()
		{
			item.width = 28;
			item.height = 30;
            item.value = Item.sellPrice(0, 3, 20, 0);
			item.rare = 6;
			item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.noFallDmg = true;
            player.GetModPlayer<NecromancyPlayer>(mod).wraith = true;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            drawColor.A = 100;
            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine t in tooltips)
            {
                string button = Main.ReversedUpDownArmorSetBonuses ? "DOWN" : "UP";
                if (t.text.StartsWith("Double tap"))
                {
                    t.text = "Double tap " + button + " and hold to fly towards your cursor for some life";
                }
            }
        }
    }
}