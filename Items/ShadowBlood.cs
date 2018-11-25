using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items
{
	public class ShadowBlood : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Blood");
        }

        public override void SetDefaults()
		{
			item.width = 20;
			item.height = 26;
			item.maxStack = 999;
			item.value = Item.sellPrice(0, 0, 2);
			item.rare = 4;
        }
    }
}