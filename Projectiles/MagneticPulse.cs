using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Necromancy.Projectiles
{
	public class MagneticPulse : ModProjectile
    {
        // kind of like magnet sphere
        // moves slowly until activated
        // if active, stops moving and becomes MagneticPulseActive
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
            for (int k = 0; k < 6; k++)
            {
                int dustIndex = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 230, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].scale *= 0.4f;
            }
            if (projectile.ai[0] == 1f)
            {
                Projectile proj = Projectile.NewProjectileDirect(projectile.Center, Vector2.Zero, mod.ProjectileType("MagneticPulseActive"), projectile.damage, projectile.knockBack, projectile.owner);
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = projectile.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom;
                projectile.Kill();
            }
        }
    }
}