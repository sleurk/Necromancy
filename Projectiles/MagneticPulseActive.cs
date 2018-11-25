using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Necromancy.Projectiles
{
	public class MagneticPulseActive : ModProjectile
    {
        // kind of like magnet sphere
        // once active, projectile will stop and start shooting at enemies

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magnetic Pulse");
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
            if (projectile.ai[0] == 0f)
            {
                projectile.ai[0] = 1f;
                Main.PlaySound(SoundID.Item25, projectile.position);
                projectile.timeLeft = 600;
                for (int k = 0; k < 15; k++)
                {
                    int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height / 2, 230, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
                    Main.dust[dustIndex].noGravity = true;
                }
                Main.PlaySound(SoundID.Item25, projectile.Center);
            }
            if (projectile.ai[1] <= 0)
            {
                NPC target = Necromancy.NearestNPC(projectile.Center);

                if (target != null && Vector2.Distance(target.Center, projectile.Center) < 200)
                {
                    float targetX = (target.Center - projectile.Center).X * 0.2f;
                    float targetY = (target.Center - projectile.Center).Y * 0.2f;
                    Projectile.NewProjectile(projectile.Center.X - 8, projectile.Center.Y - 8, targetX, targetY, mod.ProjectileType("MagneticPulseShot"), projectile.damage, projectile.knockBack, player.whoAmI);
                    projectile.ai[1] = 9;
                }                   
            }
            else
            {
                projectile.ai[1]--;
            }
            for (int k = 0; k < 8; k++)
            {
                Dust d = Dust.NewDustPerfect(projectile.Center + Main.rand.NextVector2CircularEdge(projectile.width / 2f, projectile.height / 2f), 230, Vector2.Zero, 0, default(Color), 0.5f);
                d.noGravity = true;
            }
        }
    }
}