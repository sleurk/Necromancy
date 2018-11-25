using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles.Minions
{
	public class FirewallPrime : Firewall
    {
        // NYI - summons are weird
        // This is supposed to be a sentry that creates a line of damage drawn by the player
        // This projectile is the projectile that uses minion slots and actually shoots at the other one
        private bool channeling;

        private readonly int maxLength = 300;
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Firewall");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.sentry = true;
            channeling = true;
        }

        public override void AI()
        {
            if (other == null)
            {
                Projectile proj = Projectile.NewProjectileDirect(projectile.Center, Vector2.Zero, mod.ProjectileType("Firewall"), projectile.damage, projectile.knockBack, projectile.owner, projectile.whoAmI);
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom;

                if (proj.modProjectile is Firewall)
                {
                    Firewall otherMP = (Firewall)proj.modProjectile;
                    otherMP.other = projectile;
                    other = proj;
                }

                projectile.netUpdate = true;
            }

            base.AI();

            if (Owner.channel && channeling)
            {
                if (Main.myPlayer == projectile.owner)
                {
                    projectile.velocity = Main.MouseWorld - projectile.Center;
                    projectile.netUpdate = true;

                    Vector2 newCenter = projectile.Center + projectile.velocity;
                    Vector2 toNewCenter = newCenter - other.Center;
                    if (toNewCenter.Length() > maxLength)
                    {
                        newCenter = other.Center + toNewCenter.SafeNormalize(Vector2.Zero) * maxLength;
                        projectile.velocity = newCenter - projectile.Center;
                    }
                }
            }
            else
            {
                channeling = false;
                projectile.velocity = Vector2.Zero;
                Projectile proj = Projectile.NewProjectileDirect(projectile.Center, (other.Center - projectile.Center).SafeNormalize(Vector2.Zero), mod.ProjectileType("FirewallFlame"), projectile.damage, projectile.knockBack, projectile.owner);
                proj.timeLeft = Math.Min((int)(other.Center - projectile.Center).Length(), maxLength);
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = projectile.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom;
                proj.Center = projectile.Center;
                proj.netUpdate = true;
            }
        }

        protected new bool TypeMatch()
        {
            return other.type == mod.ProjectileType("Firewall");
        }
    }
}