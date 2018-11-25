using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Projectiles.Minions
{
    public class FirewallFlame : ModProjectile
    {
        // fire projectile shot by Firewall summon
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Firewall");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.netImportant = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 2;
            projectile.hide = true;
            projectile.extraUpdates = 100;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).burn = true;
        }

        public override void AI()
        {
            if (Main.rand.NextFloat() < 0.05f)
            {
                Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 6);
                d.noGravity = true;
                d.velocity = Vector2.Zero;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 5;
        }
    }
}