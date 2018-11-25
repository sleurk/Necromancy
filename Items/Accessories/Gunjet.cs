using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace Necromancy.Items.Accessories
{
    [AutoloadEquip(EquipType.Wings)]
	public class Gunjet : ModItem
	{
        private int shootTimer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gunjet");
            Tooltip.SetDefault("'This should never work'" +
                "\nShoots downwards to propel you upward" +
                "\nLanding shots increases your flight time");
        }

		public override void SetDefaults()
		{
            item.width = 26;
			item.height = 50;
            item.value = Item.sellPrice(0, 4);
            item.rare = 6;
			item.accessory = true;
            shootTimer = 0;
        }
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
            player.wingTimeMax = 100;

            if (player.GetModPlayer<NecromancyPlayer>().wingUse > 0)
            {
                if (shootTimer <= 0)
                {
                    // Shoots a projectile down every 4 frames of flight
                    shootTimer = 4;
                    Main.PlaySound(SoundID.Item11, player.Center);
                    Vector2 vel = new Vector2(0, 32f).RotatedByRandom(MathHelper.ToRadians(3));
                    Projectile proj = Projectile.NewProjectileDirect(player.Center + new Vector2(-12f * player.direction, 8f), vel, mod.ProjectileType("GunjetShot"), 50, 0f, player.whoAmI);
                    // If this projectile hits, it adds 4 frames to the player's flight time - can be used to fly forever with consistent hits
                }
                else
                {
                    shootTimer--;
                }
            }
        }

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.1f;
			ascentWhenRising = 0.1f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 2f;
			constantAscend = 0.135f;
		}

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			speed = 8f;
			acceleration *= 1.5f;
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Megashark);
            recipe.AddIngredient(ItemID.Megashark);
            recipe.AddIngredient(ItemID.SoulofFlight, 15);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}