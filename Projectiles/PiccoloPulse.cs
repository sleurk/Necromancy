using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public abstract class PiccoloPulse : ModProjectile
	{
        // weird curvy projectile logic
        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 360;
            projectile.extraUpdates = 4;
            projectile.hide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).symphonic = true;
        }

        public abstract Color GetColor();

        public abstract float GetPerpendicularOffset();

		public override void AI()
        {
            projectile.velocity += projectile.velocity.SafeNormalize(Vector2.Zero).RotatedBy(MathHelper.PiOver2) * GetPerpendicularOffset();
            if (projectile.timeLeft > 2) Dust.QuickDust(projectile.Center, GetColor());
		}
    }
}