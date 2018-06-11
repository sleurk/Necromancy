using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class SpaceBassPulse : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Space Bass Pulse");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 6;
			projectile.height = 6;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 30;
            projectile.tileCollide = false;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).symphonic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).buffType = mod.BuffType("EmpowermentMoveSpeed");
        }

        public override void AI()
		{
            projectile.velocity *= 0.95f;
            Lighting.AddLight(projectile.Center, 0.3f, 0.2f, 0f);
        }

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 2; k++)
			{
                Main.dust[Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 6, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f)].noGravity = true;
			}
		}
    }
}