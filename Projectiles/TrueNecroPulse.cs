using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class TrueNecroPulse : ModProjectile
	{
        // NecroPulse but better, waits a short time then activates
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Necrotic Pulse");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 16;
			projectile.height = 16;
			projectile.friendly = false;
			projectile.penetrate = 3;
			projectile.timeLeft = 70;
            projectile.hide = true;
            projectile.netImportant = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).cursedfire = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 5;
        }

        public override void AI()
		{
			if (projectile.timeLeft <= 40)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 107, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 27, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
                projectile.friendly = true;
            }
        }
    }
}