using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Accessories
{
    [AutoloadEquip(EquipType.Wings)]
	public class WormholeWings : ModItem
	{
        private int healthTick;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wormhole Wings");
            Tooltip.SetDefault("Can use health to fly for longer");
        }

		public override void SetDefaults()
		{
            item.width = 24;
			item.height = 26;
            item.value = Item.sellPrice(0, 10);
            item.rare = 10;
			item.accessory = true;
            healthTick = 0;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 130;
            if (player.GetModPlayer<NecromancyPlayer>().wingUse > 0)
            {
                if (player.wingTime < 5)
                {
                    player.wingTime++;
                    if (healthTick <= 0)
                    {
                        healthTick = 4;
                        Necromancy.DrainLife(player, 1);
                    }
                    else
                    {
                        healthTick--;
                    }
                }
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
            // Will use life if there is less than 6 frames of flying time remaining
            if (inUse && player.wingTime < 6)
            {
                Main.PlaySound(SoundID.Item3, player.Center);
                Vector2 dustPos = new Vector2(Main.rand.NextFloat(player.width * 2) - player.width * player.direction, Main.rand.NextFloat(player.height)) + player.position;
                Dust dust = Dust.QuickDust(dustPos, new Color(0.5f, 0.36f, 1f));
                dust.noGravity = true;

                // this doesn't work yet
                // player.cWings is supposed to be the dye on the player's wings, but it doesn't work for modded wings (or at least, not these ones)
                GameShaders.Armor.GetSecondaryShader(player.cWings, player);
                dust.velocity = player.velocity;
            }
            return false;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "FragmentWormhole", 14);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}