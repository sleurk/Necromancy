using Necromancy.Empowerments;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class CelestialNote : ModProjectile
	{
        // similar to vanilla harp projectile, bounces & moves at a set rate
        // weapon shoots them so they align in groups of 4 if sitting still
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Note");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 22;
			projectile.height = 30;
			projectile.friendly = true;
			projectile.penetrate = 5;
			projectile.timeLeft = 600;
            projectile.netImportant = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).symphonic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).empowermentType = EmpType.Flight;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.spriteDirection *= -1;
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }

            return false;
        }

        public override void AI()
		{
			for (int i = 0; i < 3; i++)
			{
				Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 62, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f).noGravity = true;
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 8; k++)
			{
				Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 62, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f).noGravity = true;
			}
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 5;
        }
    }
}