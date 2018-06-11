using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class NatureMine : ModProjectile
    {
        private bool active;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nature Mine");
            Main.projFrames[mod.ProjectileType("NatureMine")] = 2;
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 16;
			projectile.height = 16;
			projectile.penetrate = 1;
			projectile.timeLeft = 1200;
            projectile.friendly = false;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
        }

        public override void AI()
        {
            projectile.frame = 0;
            projectile.velocity *= 0.95f;
            if (projectile.timeLeft < 1140)
            {
                if (!active)
                {
                    active = true;
                    Main.PlaySound(SoundID.Item11, projectile.Center);
                    projectile.friendly = true;
                    for (int i = 0; i < 8; i++)
                    {
                        Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 44, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
                    }
                }
                projectile.frame = 1;
            }
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

            return false;
        }

        public override void Kill(int timeLeft)
        {
            projectile.penetrate = 0;
            Explode();
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Explode();
        }

        private void Explode()
        {
            if (projectile.penetrate == 0)
            {
                Vector2 center = projectile.Center;
                projectile.width = 96;
                projectile.height = 96;
                projectile.Center = center;
                projectile.velocity *= 0;
                projectile.timeLeft = 1;
                projectile.penetrate = -1;
                Main.PlaySound(SoundID.Item41, projectile.Center);
                for (int i = 0; i < 8; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[dustIndex].velocity *= 1.4f;
                }
                for (int i = 0; i < 4; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 64, 0f, 0f, 100, default(Color), 3f);
                    Main.dust[dustIndex].noGravity = true;
                    Main.dust[dustIndex].velocity *= 5f;
                    dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 64, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[dustIndex].velocity *= 3f;

                    dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 61, 0f, 0f, 100, default(Color), 3f);
                    Main.dust[dustIndex].noGravity = true;
                    Main.dust[dustIndex].velocity *= 5f;
                    dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 61, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[dustIndex].velocity *= 3f;
                }
                for (int g = 0; g < 3; g++)
                {
                    int goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = .75f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                }
            }
        }
    }
}