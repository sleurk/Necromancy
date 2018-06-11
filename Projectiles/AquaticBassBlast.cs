using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class AquaticBassBlast : ModProjectile
	{
        int dustType = 54;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquatic Bass Blast");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
            projectile.hide = true;
            projectile.extraUpdates = 2;
            projectile.tileCollide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).symphonic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).buffType = mod.BuffType<Buffs.EmpowermentNecroticDamage>();
        }

		public override void AI()
        {
            projectile.velocity *= 0.99f;
            NPC target = Necromancy.NearestNPC(projectile.Center);
            if (target != null)
            {
                Vector2 toTarget = target.Center - projectile.Center;
                if (toTarget.Length() > 0 && toTarget.Length() < 150f)
                {
                    projectile.velocity += toTarget * 0.01f;
                }
            }
            Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, dustType).noGravity = true;
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
            {
                Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, dustType).noGravity = true;
            }
		}
    }
}