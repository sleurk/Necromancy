using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class AcidSpray : ModProjectile
	{
        // spray projectile like aqua scepter
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acid Spray");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 4;
			projectile.height = 4;
            projectile.extraUpdates = 2;
			projectile.friendly = true;
			projectile.penetrate = 10;
			projectile.timeLeft = 300;
            projectile.hide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
        }

		public override void AI()
		{
            // mostly vanilla code from aqua scepter
            projectile.scale -= 0.02f;
            if (projectile.scale <= 0f)
            {
                projectile.Kill();
            }
            if (projectile.ai[0] > 3f)
            {
                projectile.velocity.Y = projectile.velocity.Y + 0.2f;
                int num3;
                for (int num159 = 0; num159 < 1; num159 = num3 + 1)
                {
                    for (int num160 = 0; num160 < 3; num160 = num3 + 1)
                    {
                        float num161 = projectile.velocity.X / 3f * (float)num160;
                        float num162 = projectile.velocity.Y / 3f * (float)num160;
                        int num163 = 6;
                        int num164 = Dust.NewDust(new Vector2(projectile.position.X + (float)num163, projectile.position.Y + (float)num163), projectile.width - num163 * 2, projectile.height - num163 * 2, 44, 0f, 0f, 100, default(Color), 1.2f);
                        Main.dust[num164].noGravity = true;
                        Dust dust3 = Main.dust[num164];
                        dust3.velocity *= 0.3f;
                        dust3 = Main.dust[num164];
                        dust3.velocity += projectile.velocity * 0.5f;
                        Dust dust22 = Main.dust[num164];
                        dust22.position.X = dust22.position.X - num161;
                        Dust dust23 = Main.dust[num164];
                        dust23.position.Y = dust23.position.Y - num162;
                        num3 = num160;
                    }
                    if (Main.rand.Next(8) == 0)
                    {
                        int num165 = 6;
                        int num166 = Dust.NewDust(new Vector2(projectile.position.X + (float)num165, projectile.position.Y + (float)num165), projectile.width - num165 * 2, projectile.height - num165 * 2, 44, 0f, 0f, 100, default(Color), 0.75f);
                        Dust dust3 = Main.dust[num166];
                        dust3.velocity *= 0.5f;
                        dust3 = Main.dust[num166];
                        dust3.velocity += projectile.velocity * 0.5f;
                    }
                    num3 = num159;
                }
                return;
            }
            projectile.ai[0] += 1f;
            return;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.CursedInferno, 300);
            target.AddBuff(BuffID.Ichor, 300);
        }
    }
}