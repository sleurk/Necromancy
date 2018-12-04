using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class MagneticPulseShot : ModProjectile
	{
        // basic homing projectile
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magnetic Pulse Shot");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 16;
			projectile.height = 16;
			projectile.friendly = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            projectile.penetrate = 1;
			projectile.timeLeft = 150;
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
                    projectile.velocity.X = 0.95f * projectile.velocity.X + toTarget.X * 0.1f;
                    projectile.velocity.Y = 0.95f * projectile.velocity.Y + toTarget.Y * 0.1f;
                }
            }

            for (int k = 0; k < 10; k++)
            {
                int dustIndex = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 230, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].scale *= 0.4f;
            }
        }

        public override void Kill(int timeLeft)
		{
            for (int k = 0; k < 15; k++)
            {
                int dustIndex = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 230, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].scale *= 0.6f;
            }
        }

        public override bool PreAI()
        {
            Lighting.AddLight(projectile.position, .6f, .2f, .2f);
            return true;
        }
    }
}