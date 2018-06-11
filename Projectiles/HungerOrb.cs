using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class HungerOrb : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hunger Orb");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 16;
			projectile.height = 16;
			projectile.friendly = true;
            projectile.netImportant = true;
			projectile.penetrate = 3;
			projectile.timeLeft = 300;
            projectile.alpha = 100;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
        }

		public override void AI()
		{
            bool targeting = false;
            projectile.velocity *= 0.95f;
            NPC target = Necromancy.NearestNPC(projectile.Center);
            if (target != null)
            {
                Vector2 toTarget = target.Center - projectile.Center; // ??????
                if (toTarget.Length() > 0 && toTarget.Length() < 200)
                {
                    targeting = true;
                    projectile.velocity += toTarget * 0.02f;
                }
            }
            if (!targeting)
            {
                projectile.netUpdate = true;
                projectile.velocity += Vector2.UnitX.RotatedByRandom(MathHelper.ToRadians(360));
            }
		    Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 60, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }
            Main.PlaySound(SoundID.Item10, projectile.position);
            
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 5;
        }

        public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 60, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
			}
		}

        public override bool PreAI()
        {
            Lighting.AddLight(projectile.position, .6f, .2f, .2f);
            return true;
        }
    }
}