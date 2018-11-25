using Necromancy.Empowerments;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class BrassPulse : ModProjectile
	{
        // basic projectile that moves slowly
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brass Pulse");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 40;
			projectile.height = 40;
			projectile.friendly = true;
			projectile.penetrate = -1;
            projectile.hide = true;
			projectile.timeLeft = 1200;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).symphonic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).empowermentType = EmpType.Damage;
        }

		public override void AI()
		{
            for (int k = 0; k < 15; k++)
            {
                Dust d = Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 60, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
                d.noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 20; k++)
            {
                Dust d = Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 60, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
                d.noGravity = true;
            }
		}

        public override bool PreAI()
        {
            Lighting.AddLight(projectile.position, .8f, 0f, 0f);
            return true;
        }
    }
}