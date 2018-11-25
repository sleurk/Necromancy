using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class SubBub : ModProjectile
	{
        // projectile that stays and orbits the player at a long range until it hits something

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sub Bubble");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 20;
			projectile.height = 20;
			projectile.friendly = true;
            projectile.tileCollide = false;
			projectile.penetrate = 2;
			projectile.timeLeft = 1200;
            projectile.extraUpdates = 1;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).ranged = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).lifeSteal = 4;
        }

		public override void AI()
		{
            if (projectile.ai[0] == 1f)
            {
                Vector2 toPlayer = Main.player[projectile.owner].Center - projectile.Center;
                if (toPlayer.Length() < 32f)
                {
                    projectile.ai[0] = 0f;
                }
                projectile.velocity = projectile.velocity * 0.999f + toPlayer * 0.001f;
                if (projectile.velocity.Length() > 16f)
                {
                    projectile.velocity.Normalize();
                    projectile.velocity *= 16f;
                }
            }
            else
            {
                projectile.ai[1]++;
                if (projectile.ai[1] == 20)
                {
                    projectile.ai[0] = 1f;
                    projectile.ai[1] = 0f;
                }
            }
            if (Main.rand.NextBool()) Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 156, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f).scale = Main.rand.NextFloat(0.2f, 0.8f);
        }

        public override bool PreAI()
        {
            Lighting.AddLight(projectile.position, 0f, 0.6f, 0.5f);
            return true;
        }
    }
}