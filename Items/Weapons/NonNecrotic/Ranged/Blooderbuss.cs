using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;

namespace Necromancy.Items.Weapons.NonNecrotic.Ranged
{
	public class Blooderbuss : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blooderbuss");
        }

        public override void SetDefaults()
        {
            item.ranged = true;
            item.damage = 61;
            item.width = 54;
            item.height = 26;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 8;
            item.value = Item.buyPrice(1);
            item.rare = 4;
            item.UseSound = SoundID.Item36;
            item.shoot = 10;
            item.shootSpeed = 1f;
            item.prefix = 0;
            item.useAmmo = AmmoID.Bullet;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            type = mod.ProjectileType("BlooderbussShot");

            Vector2 vel = new Vector2(speedX, speedY);
            position += vel * (88f + 24f);
            position.Y -= 4f * Math.Sign(speedX);

            // creates projectile further out and moved vertically to align with gun

            Projectile proj = Projectile.NewProjectileDirect(position, vel, type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8f, 0f);
        }
    }
}
