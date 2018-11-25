using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;

namespace Necromancy.Items.Weapons.Ranged
{
	public class GaiaGatlingGun : ModItem
	{
        private float inaccuracy;
        private int shootCounter;
        private int shootTimer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gaia's Gatling Gun");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 34;
            item.width = 76;
            item.height = 46;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 5;
            item.noMelee = true;
            item.autoReuse = true;
            item.knockBack = 1;
            item.value = Item.buyPrice(1);
            item.rare = 5;
            item.UseSound = SoundID.Item41;
            item.shoot = mod.ProjectileType("GaiaBullet");
            item.shootSpeed = 2f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).ranged = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeCost = 3;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal = 3;
            inaccuracy = MathHelper.Pi / 8f;
            shootCounter = 0;
            shootTimer = 0;
        }

        public override void HoldItem(Player player)
        {
            // changes accuracy and firing speed while shooting, this checks if it is shooting
            shootTimer = Math.Max(0, shootTimer - 1);
            if (shootTimer == 0) // if it is not shooting
            {
                // reset accuracy and firing speed
                inaccuracy = MathHelper.Pi / 8f;
                item.useTime = 10;
                item.useAnimation = 10;
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position += new Vector2(speedX, speedY) * 26f + new Vector2(0f, 5f) + Vector2.UnitX.RotatedBy(shootCounter * MathHelper.TwoPi / 8f) * 6f;
            shootTimer = 12; // update the item to show it is still shooting (for HoldItem)
            shootCounter++;
            if (shootCounter == 4) // every 4 shots, accuracy and firing speed increase
            {
                shootCounter = 0;
                item.useTime = Math.Max(3, item.useTime - 1);
                item.useAnimation = Math.Max(3, item.useAnimation - 1);
                inaccuracy *= 0.93f;
                if (shootCounter == 8) shootCounter = 0;
            }
            // bullet spread = inaccuracy, some variance in bullet speed
            Vector2 vel = new Vector2(speedX, speedY).RotatedByRandom(inaccuracy) * Main.rand.NextFloat(0.8f, 1.1f);
            Projectile proj = Projectile.NewProjectileDirect(position, vel, type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 5);
        }
    }
}
