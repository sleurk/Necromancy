using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items
{
	public class BrokenHeroTome : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Broken Hero Tome");
        }

        public override void SetDefaults()
		{
			item.width = 28;
			item.height = 30;
			item.maxStack = 99;
			item.value = Item.sellPrice(0, 2, 0, 0);
			item.rare = 8;
		}
    }
}