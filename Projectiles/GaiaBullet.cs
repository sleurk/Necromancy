using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class GaiaBullet : ModProjectile
	{
        // basic bullet, shot with increasing rate and accuracy
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gaia Bullet");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 8;
			projectile.height = 8;
            projectile.friendly = true;
            projectile.penetrate = 1;
			projectile.timeLeft = 3600;
            projectile.extraUpdates = 48;
            projectile.hide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).ranged = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).glow = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).lifeSteal = 3;

        }

		public override void AI()
        {
            Dust d = Dust.QuickDust(projectile.Center, new Color(0.5f, 1f, 0f));
            d.velocity = projectile.velocity * 0.5f;
        }
    }
}