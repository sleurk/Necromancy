using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class Flame : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flame");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 16;
			projectile.height = 16;
			projectile.friendly = true;
			projectile.penetrate = 10;
			projectile.timeLeft = 30;
            projectile.hide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
        }

		public override void AI()
		{
            projectile.velocity *= 0.9f;
            for (int k = 0; k < 5; k++)
            {
                Main.dust[Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 6, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f)].noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 20; k++)
            {
                Main.dust[Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 6, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f)].noGravity = true;
            }
		}

        public override bool PreAI()
        {
            Lighting.AddLight(projectile.position, .4f, .3f, 0f);
            return true;
        }
    }
}