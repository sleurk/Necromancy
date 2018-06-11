using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class LifeDisruption : ModProjectile
	{
        private int time = 0;

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
            time++;
            if (time >= 15)
            {
                time = 0;
                Vector2 shoot = 8f * Vector2.UnitX.RotatedBy(projectile.ai[0]);
                Projectile.NewProjectileDirect(projectile.Center, shoot + projectile.velocity, mod.ProjectileType("LifeShot"), projectile.damage, projectile.knockBack, projectile.owner);
                Projectile.NewProjectileDirect(projectile.Center, -shoot + projectile.velocity, mod.ProjectileType("LifeShot"), projectile.damage, projectile.knockBack, projectile.owner);
                projectile.ai[0] += 0.3f;
            }

            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 61, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
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