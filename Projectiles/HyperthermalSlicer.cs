using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class HyperthermalSlicer : ModProjectile
	{
        private bool bounce;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hyperthermal Slicer");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 40;
			projectile.height = 40;
			projectile.friendly = true;
			projectile.penetrate = 10;
			projectile.timeLeft = 180;
            projectile.extraUpdates = 8;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).radiant = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).healPower = 3;
        }

		public override void AI()
		{
            if (projectile.penetrate < 6)
            {
                bounce = true;
            }
            projectile.rotation += projectile.direction;
            if (bounce)
            {
                projectile.penetrate = -1;
                Vector2 toPlayer = Main.player[projectile.owner].Center - projectile.Center;
                if (toPlayer.Length() < 32f)
                {
                    projectile.Kill();
                }
                toPlayer.Normalize();
                projectile.velocity = projectile.velocity * 0.9f + toPlayer * 2f;
            }
            else
            {
                if (projectile.timeLeft < 105)
                {
                    bounce = true;
                }
            }
            Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 158, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f).noGravity = true;
            Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 158, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f).noGravity = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            bounce = true;
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }
            Main.PlaySound(SoundID.Item10, projectile.position);
            
            return false;
        }
    }
}