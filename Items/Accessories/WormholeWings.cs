using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Accessories
{
    [AutoloadEquip(EquipType.Wings)]
	public class WormholeWings : ModItem
	{
        private int healthTick;
        private bool glow;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wormhole Wings");
            Tooltip.SetDefault("Can use health to fly for longer");
        }

		public override void SetDefaults()
		{
            item.width = 24;
			item.height = 26;
			item.value = Item.sellPrice(0, 8, 0, 0);
			item.rare = 10;
			item.accessory = true;
            healthTick = 0;
            glow = false;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 130;
            if (player.GetModPlayer<NecromancyPlayer>().wingUse > 0)
            {
                if (player.wingTime < 5)
                {
                    glow = true;
                    player.wingTime++;
                    if (healthTick <= 0)
                    {
                        healthTick = 4;
                        Necromancy.DrainLife(player, 1);
                    }
                    else
                    {
                        glow = false;
                        healthTick--;
                    }
                }
            }
            else
            {
                glow = false;
            }
        }

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.6f;
			ascentWhenRising = 0.1f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 3f;
			constantAscend = 0.135f;
		}

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			speed = 9f;
			acceleration *= 2.5f;
		}

        public override bool WingUpdate(Player player, bool inUse)
        {

            return false;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "FragmentWormhole", 14);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}