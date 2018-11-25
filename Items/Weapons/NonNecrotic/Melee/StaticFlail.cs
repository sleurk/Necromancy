using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.NonNecrotic.Melee
{
	public class StaticFlail : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Static Flail");
            Tooltip.SetDefault("High damage at high speeds");
        }

        public override void SetDefaults()
        {
            item.melee = true;
            item.damage = 71;
            item.width = 32;
			item.height = 32;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = 5;
			item.knockBack = 6;
            item.channel = true;
            item.noUseGraphic = true;
			item.useTurn = false;﻿
            item.noMelee = true;
			item.rare = 8;
            item.value = Item.buyPrice(1);
            item.UseSound = SoundID.Item1;
			item.shoot = mod.ProjectileType("StaticFlailBall");
			item.shootSpeed = 2.4f;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            // shoots 3 randomly spread flails, then 'connects' them with ai[0]
            int numberProjectiles = 3;
            int[] projectiles = new int[numberProjectiles];
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30));
                float scale = 1f - (Main.rand.NextFloat() * .3f);
                perturbedSpeed = perturbedSpeed * scale;
                Projectile proj = Projectile.NewProjectileDirect(position, perturbedSpeed, type, damage, knockBack, player.whoAmI);
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
                projectiles[i] = proj.whoAmI;
            }
            for (int i = 0; i < numberProjectiles; i++)
            {
                /* 
                  Projectile's state was modified after it was created, and Shoot is client-only besides the creation (Projectile.NewProjectileDirect),
                  so netUpdate is flagged. This tells the server and other clients to match the projectile's state with the state of the projectile
                  on the projectile's owner's client.
                 */
                Main.projectile[projectiles[i]].ai[0] = projectiles[(i + 1) % numberProjectiles];
                Main.projectile[projectiles[i]].netUpdate = true;
            }
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            // can only use if there are no existing projectiles from this item
            return player.ownedProjectileCounts[item.shoot] == 0;
		}
    }
}
