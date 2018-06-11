using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class PrideRing : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pride Ring");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 4;
			projectile.height = 4;
            projectile.hide = true;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
        }

		public override void AI()
        {
            Vector2 ringVel = new HalfVector2() { PackedValue = (uint)projectile.ai[1] }.ToVector2();
            Vector2 ringCenter = projectile.Center - Vector2.UnitX.RotatedBy(projectile.ai[0]) * 12f;
            projectile.ai[0] += 0.1f;
            Vector2 nextPos = ringCenter + Vector2.UnitX.RotatedBy(projectile.ai[0]) * 12f;
            projectile.velocity = nextPos - projectile.Center;
            projectile.velocity += ringVel;
			Dust d = Dust.NewDustPerfect(projectile.Center, 62);
            d.velocity = ringVel * 0.75f;
            d.scale = Main.rand.NextFloat(0.5f, 0.7f);
            d.noGravity = true;
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 62, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
			}
		}
    }
}