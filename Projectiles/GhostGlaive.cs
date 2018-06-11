using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class GhostGlaive : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ghost Glaive");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 52;
			projectile.height = 52;
			projectile.friendly = true;
			projectile.penetrate = 3;
			projectile.timeLeft = 120;
            projectile.tileCollide = false;
            projectile.alpha = 100;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).throwing = true;
        }

        public override void AI()
        {
            projectile.alpha += 1;
            projectile.rotation -= 0.42f;
            projectile.velocity.Y += 0.2f;
            Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 21, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 21, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
                Main.dust[dust].noGravity = true;
                if (Main.rand.Next(4) == 0)
                {
                    Main.dust[dust].scale *= 0.5f;
                }
            }
        }
    }
}