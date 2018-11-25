using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class LifeDisruption : ModProjectile
	{
        // moves in an arc, shoots many homing projectiles
        // will dissipate if it hits something

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Life Disruption");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 32;
			projectile.height = 32;
			projectile.friendly = true;
			projectile.penetrate = 1;
            projectile.hide = true;
            projectile.timeLeft = 600;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
        }

		public override void AI()
		{
            projectile.velocity.Y += 0.05f;
            projectile.ai[0]++;
            if (projectile.ai[0] >= 15)
            {
                projectile.ai[0] = 0;
                Vector2 shoot1 = projectile.velocity.SafeNormalize(Vector2.Zero).RotatedBy(MathHelper.PiOver2);
                Vector2 shoot2 = projectile.velocity.SafeNormalize(Vector2.Zero).RotatedBy(-MathHelper.PiOver2);
                for (int i = 1; i <= 3; i++)
                {
                    Projectile.NewProjectileDirect(projectile.Center, shoot1, mod.ProjectileType("LifeShot"), projectile.damage, projectile.knockBack, projectile.owner);
                    Projectile.NewProjectileDirect(projectile.Center, shoot2, mod.ProjectileType("LifeShot"), projectile.damage, projectile.knockBack, projectile.owner);
                    shoot1 *= 2f;
                    shoot2 *= 2f;
                }
            }

            for (int k = 0; k < 10; k++)
            {
                Dust d = Dust.QuickDust(projectile.Center + Main.rand.NextVector2CircularEdge(projectile.width / 2, projectile.height / 2), Color.Green);
                d.velocity = 0.2f * (projectile.Center - d.position);
            }
        }

        public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 20; k++)
			{
				Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 61, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
			}
		}

        public override bool PreAI()
        {
            Lighting.AddLight(projectile.position, 0f, .7f, 0f);
            return true;
        }
    }
}