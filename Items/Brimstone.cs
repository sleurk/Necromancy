using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items
{
	public class Brimstone : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brimstone");
        }

        public override void SetDefaults()
		{
			item.width = 24;
			item.height = 16;
			item.maxStack = 999;
			item.value = Item.sellPrice(0, 0, 5, 0);
			item.rare = 3;
		}
	}
}