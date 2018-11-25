using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class NecroPulse : ModProjectile
	{
        // from necronomicon, waits a short time, then activates
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Necrotic Pulse");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 16;
			projectile.height = 16;
			projectile.friendly = false;
            projectile.netImportant = true;
			projectile.penetrate = 3;
			projectile.timeLeft = 40;
            projectile.hide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 5;
        }

        public override void AI()
		{
			if (projectile.timeLeft <= 10)
            {
                for (int i = 0; i < 4; i++)
                {
                    Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 27, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
                }
                projectile.friendly = true;
            }
		}
    }
}