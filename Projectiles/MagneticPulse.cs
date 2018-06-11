using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Necromancy.Projectiles
{
	public class MagneticPulse : ModProjectile
    {
        private int shootTimer = 0;
        private bool secondStage = false;
        private int animTimer = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magnetic Pulse");
            Main.projFrames[mod.ProjectileType("MagneticPulse")] = 6;
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.netImportant = true;
            projectile.width = 32;
			projectile.height = 32;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
            projectile.hide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
        }

		public override void AI()
        {
            Player player = Main.player[projectile.owner];
            for (int k = 0; k < 6; k++)
            {
                int dustIndex = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 230, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].scale *= 0.4f;
            }
            if (projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magnetActivated)
            {
                if (!secondStage)
                {
                    projectile.hide = true;
                    projectile.netUpdate = true;
                    Main.PlaySound(SoundID.Item25, projectile.position);
                    secondStage = true;
                    projectile.timeLeft = 600;
                    for (int k = 0; k < 15; k++)
                    {
                        int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height / 2, 230, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
                        Main.dust[dustIndex].noGravity = true;
                    }
                    Main.PlaySound(SoundID.Item25, projectile.Center);
                    projectile.velocity *= 0f;
                    secondStage = true;
                }
                if (shootTimer <= 0)
                {
                    NPC target = Necromancy.NearestNPC(projectile.Center);

                    if (target != null && Vector2.Distance(target.Center, projectile.Center) < 200)
                    {
                        float targetX = (target.Center - projectile.Center).X * 0.2f;
                        float targetY = (target.Center - projectile.Center).Y * 0.2f;
                        Projectile.NewProjectile(projectile.Center.X - 8, projectile.Center.Y - 8, targetX, targetY, mod.ProjectileType("MagneticPulseShot"), projectile.damage, projectile.knockBack, player.whoAmI);
                        shootTimer = 9;
                    }
                    for (int k = 0; k < 15; k++)
                    {
                        int dustIndex = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 230, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
                        Main.dust[dustIndex].noGravity = true;
                        Main.dust[dustIndex].scale *= 0.8f;
                    }                    
                }
                else
                {
                    shootTimer--;
                    for (int k = 0; k < 15; k++)
                    {
                        int dustIndex = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 230, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
                        Main.dust[dustIndex].noGravity = true;
                        Main.dust[dustIndex].scale *= 0.5f;
                    }
                }
            }
            animTimer++;
            if (animTimer >= 18)
            {
                animTimer = 0;
            }
            projectile.frame = animTimer / 3;
        }

		public override void Kill(int timeLeft)
		{
            if (projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magnetActivated)
            {
                Main.player[projectile.owner].GetModPlayer<NecromancyPlayer>(mod).magnetsActive = false;
            }
            Main.PlaySound(SoundID.Item25, projectile.Center);
            Main.player[projectile.owner].GetModPlayer<NecromancyPlayer>(mod).magnets.Remove(projectile);
            for (int k = 0; k < 15; k++)
            {
                int dustIndex = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 230, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].scale *= 0.7f;
            }
        }

        public override bool PreAI()
        {
            Lighting.AddLight(projectile.position, .4f, .4f, .9f);
            return true;
        }
    }
}