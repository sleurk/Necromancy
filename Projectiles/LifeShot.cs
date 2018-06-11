using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class LifeShot : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Life Shot");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 16;
			projectile.height = 16;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 60;
            projectile.hide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
        }

		public override void AI()
        {
            NPC target = Necromancy.NearestNPC(projectile.Center);
            if (target != null)
            {
                Vector2 toTarget = target.Center - projectile.Center;
                if (toTarget.Length() > 0 && toTarget.Length() < 200)
                {
                    projectile.velocity.X = 0.98f * projectile.velocity.X + toTarget.X * 0.02f;
                    projectile.velocity.Y = 0.98f * projectile.velocity.Y + toTarget.Y * 0.02f;
                }
                projectile.velocity.Normalize();
                projectile.velocity *= projectile.oldVelocity.Length();
            }

            Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 61, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 61, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 61, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
			}
		}
    }
}