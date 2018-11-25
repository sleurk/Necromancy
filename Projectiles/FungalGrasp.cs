using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Projectiles
{
	public class FungalGrasp : ModProjectile
	{
        // mimics shadowflame hex doll, fancy mushroom visuals

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fungal Grasp");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 32;
			projectile.height = 32;
            projectile.friendly = true;
            projectile.penetrate = -1;
			projectile.timeLeft = 320;
            projectile.hide = true;
            projectile.netImportant = true;
            projectile.extraUpdates = 32;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).glow = true;
        }

		public override void AI()
        {
            projectile.velocity = projectile.velocity.RotatedBy(projectile.ai[0] * 0.003f);

            if (projectile.timeLeft % 2 == 0)
            {
                Vector2 offset = projectile.velocity.RotatedBy(MathHelper.PiOver2).SafeNormalize(Vector2.Zero) * (projectile.timeLeft + 200) / 15f;

                int direction = ((projectile.timeLeft / 2) % 2) * 2 - 1;
                Dust d = Dust.QuickDust(projectile.Center + offset * direction, new Color(0.5f, 1f, 0f));
                d.velocity = 0.08f * (Main.player[projectile.owner].Center - d.position);
            }
        }

        public override bool PreKill(int timeLeft)
        {
            if (projectile.ai[0] == 0f)
            {
                float mushroomHeight = Main.rand.NextFloat(0.8f, 1.2f);
                int mushroomWidth = Main.rand.Next(48, 96);
                for (int i = -mushroomWidth; i <= mushroomWidth; i += 4)
                {
                    Vector2 semicircle = mushroomWidth * Vector2.UnitX.RotatedBy(i * Math.PI / (mushroomWidth * 2));
                    semicircle.X *= (float)Math.Pow(mushroomHeight, 6);
                    Dust d = Dust.QuickDust(projectile.Center + semicircle.RotatedBy(projectile.velocity.ToRotation()), new Color(0.5f, 1f, 0f));
                    d.velocity = 0.08f * (Main.player[projectile.owner].Center - d.position);

                    if (Math.Abs(i) > (timeLeft + 200) / 15f)
                    {
                        Vector2 line = new Vector2(0f, i);
                        d = Dust.QuickDust(projectile.Center + line.RotatedBy(projectile.velocity.ToRotation()), new Color(0.5f, 1f, 0f));
                        d.velocity = 0.08f * (Main.player[projectile.owner].Center - d.position);
                    }
                }
                projectile.timeLeft = 3;
                projectile.ai[0] = 1f;
                return true;
            }
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 4;
        }
    }
}