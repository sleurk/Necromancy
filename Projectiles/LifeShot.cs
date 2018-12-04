using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class LifeShot : ModProjectile
	{
        // basic projectile, homes
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
            ProjectileID.Sets.Homing[projectile.type] = true;
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
                if (toTarget.LengthSquared() > 0 && toTarget.LengthSquared() < 200f * 200f)
                {
                    projectile.velocity.X = 0.98f * projectile.velocity.X + toTarget.X * 0.02f;
                    projectile.velocity.Y = 0.98f * projectile.velocity.Y + toTarget.Y * 0.02f;
                }
                projectile.velocity.Normalize();
                projectile.velocity *= projectile.oldVelocity.Length();
            }

            Dust d = Dust.QuickDust(projectile.Center, Color.Green);
        }

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
            {
                Dust d = Dust.QuickDust(projectile.Center + Main.rand.NextVector2CircularEdge(projectile.width / 2, projectile.height / 2), Color.Green);
            }
		}
    }
}