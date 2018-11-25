using Necromancy.Empowerments;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Projectiles
{
	public class GoongWave : ModProjectile
	{
        // creates a circular wave
        // moves in a very fast expanding circle around the player
        private const int LIFESPAN = 10800;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Goong Wave");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            // thorium.symphonic = true
            projectile.width = 16;
			projectile.height = 16;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.netImportant = true;
            projectile.penetrate = -1;
			projectile.timeLeft = LIFESPAN; // 1 second
            projectile.hide = true;
            projectile.extraUpdates = 180;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).goo = true;
        }

        public override void AI()
		{
            Vector2 center = new Vector2(projectile.ai[0], projectile.ai[1]);
            float maxRadius = 400f;
            float radius = maxRadius * (LIFESPAN - projectile.timeLeft) / 1f / LIFESPAN + 32f;

            Vector2 newPos = center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(projectile.timeLeft % 360)) * radius;
            projectile.velocity = newPos - projectile.Center;

            if (projectile.timeLeft % 7 == 0)
            {
                Dust d = Dust.QuickDust(projectile.Center + projectile.velocity, new Color(0.1f, 1f, 0.2f));
                d.velocity = projectile.velocity.RotatedBy(MathHelper.PiOver2).SafeNormalize(Vector2.Zero) * 4f;
                d.noGravity = true;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Necromancy.DoCustomKnockback(target, projectile.velocity.RotatedBy(MathHelper.PiOver2));
            target.immune[projectile.owner] = 5;
        }
    }
}