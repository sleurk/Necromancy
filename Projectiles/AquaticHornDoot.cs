using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class AquaticHornDoot : ModProjectile
	{
        int dustType = 61;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquatic Horn Doot");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 600;
            projectile.hide = true;
            projectile.extraUpdates = 3;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).symphonic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).buffType = mod.BuffType<Buffs.EmpowermentRangedDamage>();
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (oldVelocity.X != projectile.velocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (oldVelocity.Y != projectile.velocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }
            projectile.velocity *= 0.85f;
            return false;
        }

        public override void AI()
        {
            for (int i = 0; i < 3; i++)
            {
                Dust d = Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, dustType);
                d.noGravity = true;
                d.velocity = projectile.velocity * Main.rand.NextFloat(0.5f, 0.75f);
            }
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