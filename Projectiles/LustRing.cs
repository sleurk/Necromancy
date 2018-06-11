using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class LustRing : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lust Ring");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 16;
			projectile.height = 16;
			projectile.friendly = true;
            projectile.tileCollide = false;
			projectile.penetrate = 1;
			projectile.timeLeft = 180;
            projectile.hide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
        }

		public override void AI()
		{
            for (int i = 0; i < 3; i++)
            {
                Dust d = Dust.NewDustPerfect(projectile.Center + Main.rand.NextVector2CircularEdge(8, 8), 71, projectile.velocity);
                d.noGravity = true;
                d.scale *= 0.75f;
            }
		}

        public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 71, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
			}
		}
    }
}