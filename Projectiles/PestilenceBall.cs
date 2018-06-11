using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class PestilenceBall : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pestilence Ball");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 16;
			projectile.height = 16;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
        }

		public override void AI()
		{
            projectile.rotation += projectile.direction;
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 54, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f).noGravity = true;
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
                Dust dust = Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 54, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
                dust.velocity *= 1.8f;
                dust.noGravity = true;
                dust.velocity.Y -= 0.5f;
                if (Main.rand.Next(4) == 0)
                {
                    dust.scale *= 0.75f;
                }
            }
			Main.PlaySound(SoundID.Item25, projectile.position);
		}
    }
}