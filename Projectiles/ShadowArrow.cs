using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class ShadowArrow : ModProjectile
	{
        // basic arrow projectile
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Arrow");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 10;
			projectile.height = 10;
            projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 60;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).ranged = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).lifeSteal = 4;
        }

        public override void AI()
        {
            Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 135, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f).noGravity = true;
        }

        public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
            {
                Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 135, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f).noGravity = true;
            }
		}
    }
}