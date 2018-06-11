using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class WrathRing : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wrath Ring");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 32;
			projectile.height = 32;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 300;
            projectile.hide = true;
            projectile.netImportant = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
        }

		public override void AI()
		{
            projectile.velocity *= 0.998f;
            if (Main.myPlayer == projectile.owner)
            {
                projectile.netUpdate = true;
                Vector2 toTarget = Main.MouseWorld - projectile.Center; // ??????
                projectile.velocity += toTarget * 0.002f;
            }
            for (int i = 0; i < 6; i++) Dust.NewDustPerfect(projectile.Center + Main.rand.NextVector2CircularEdge(16, 16), 60, projectile.velocity).noGravity = true;
		}

        public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 60, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
			}
		}
    }
}