using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class RedStar : ModProjectile
	{
        // basic thrown gravity projectile

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red Star");
        }

        public override void SetDefaults()
        {
            projectile.thrown = true;
            projectile.width = 22;
			projectile.height = 22;
			projectile.friendly = true;
			projectile.penetrate = 10;
			projectile.timeLeft = 360;
            projectile.extraUpdates = 7;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).blood = true;
        }

        public override void AI()
        {
            projectile.rotation += projectile.velocity.X / 8f;
            Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 5, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(0, projectile.position);
            return true;
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 5, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}