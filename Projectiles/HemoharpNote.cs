using Necromancy.Empowerments;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class HemoharpNote : ModProjectile
	{
        // basic projectile, slows to a stop
        // created in a ring from the player
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heomharp Note");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            // thorium.symphonic = true
            projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 10;
            projectile.hide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).blood = true;
        }

        public override void AI()
        {
            Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 5, projectile.velocity.X, projectile.velocity.Y).noGravity = true;
            projectile.velocity *= 0.99f;
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
            {
                Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 5).noGravity = true;
            }
		}
    }
}