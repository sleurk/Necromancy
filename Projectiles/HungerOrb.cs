using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class HungerOrb : ModProjectile
	{
        // homing projectile that lasts a short while

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
            ProjectileID.Sets.Homing[projectile.type] = true;
            projectile.penetrate = 3;
			projectile.timeLeft = 300;
            projectile.alpha = 100;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
        }

		public override void AI()
		{
            projectile.ai[0] += Main.rand.NextFloat(0f, 0.05f);
            projectile.alpha = Main.rand.Next(256);
            projectile.rotation += projectile.ai[0];
            bool targeting = false;
            projectile.velocity *= 0.95f;
            NPC target = Necromancy.NearestNPC(projectile.Center);
            if (target != null)
            {
                Vector2 toTarget = target.Center - projectile.Center; // ??????
                if (toTarget.LengthSquared() > 0 && toTarget.LengthSquared() < 200f * 200f)
                {
                    targeting = true;
                    projectile.velocity += toTarget * 0.02f;
                }
            }
            if (!targeting)
            {
                projectile.netUpdate = true;
                projectile.velocity += Main.rand.NextVector2CircularEdge(1f, 1f);
            }
            Dust.QuickDust(projectile.Center, new Color(Main.rand.NextFloat(0.4f, 0.6f), Main.rand.NextFloat(0.26f, 0.46f), Main.rand.NextFloat(0.8f, 1f))).velocity = projectile.velocity;
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
                Dust.QuickDust(projectile.Center + Main.rand.NextVector2Circular(projectile.width / 2, projectile.height / 2), new Color(0.5f, 0.36f, 1f)).velocity = Main.rand.NextVector2Circular(12f, 12f);
            }
		}

        public override bool PreAI()
        {
            Lighting.AddLight(projectile.position, .5f, .36f, 1f);
            return true;
        }
    }
}