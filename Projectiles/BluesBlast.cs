using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class BluesBlast : ModProjectile
	{
        private bool bounce = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BluesBlast");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 30;
			projectile.height = 30;
			projectile.friendly = true;
			projectile.penetrate = 30;
			projectile.timeLeft = 80;
            projectile.aiStyle = 0;
            projectile.extraUpdates = 1;
            projectile.tileCollide = false;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).symphonic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).buffType = mod.BuffType<Buffs.EmpowermentEndurance>();
        }

		public override void AI()
		{
            if (bounce)
            {
                Vector2 toPlayer = Main.player[projectile.owner].Center - projectile.Center;
                if (toPlayer.Length() < 32f)
                {
                    projectile.Kill();
                }
                toPlayer.Normalize();
                projectile.velocity = projectile.velocity * 0.95f + toPlayer;
            }
            else
            {
                if (projectile.timeLeft < 60)
                {
                    bounce = true;
                }
            }
            Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 135, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f).noGravity = true;
            Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 135, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f).noGravity = true;
        }

        public override bool PreAI()
        {
            Lighting.AddLight(projectile.position, 0f, 0.3f, 0.5f);
            return true;
        }
    }
}