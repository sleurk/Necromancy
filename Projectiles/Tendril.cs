using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Projectiles
{
	public class Tendril : ModProjectile
	{
        public NPC target;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tendril");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 4;
			projectile.height = 4;
            projectile.friendly = true;
            projectile.penetrate = 1;
			projectile.timeLeft = 160;
            projectile.hide = true;
            projectile.extraUpdates = 80;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).radiant = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).healPower = 1;
        }

        public override void AI()
        {
            if (target == null || !target.active)
            {
                projectile.Kill();
            }
            projectile.velocity = projectile.velocity * 0.999f + (target.Center - projectile.Center) * 0.001f;
            projectile.velocity.Normalize();

            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] > 2f)
            {
                int num3;
                for (int num452 = 0; num452 < 2; num452 = num3 + 1)
                {
                    Vector2 vector36 = projectile.position;
                    vector36 -= projectile.velocity * ((float)num452 * 0.25f);
                    projectile.alpha = 255;

                    int num453 = Dust.NewDust(vector36, 1, 1, 135);
                    Main.dust[num453].position = vector36;
                    Main.dust[num453].noGravity = true;
                    Main.dust[num453].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                    Dust dust3 = Main.dust[num453];
                    dust3.velocity *= 0.2f;
                    num3 = num452;
                }
            }
        }
    }
}