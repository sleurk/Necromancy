using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class AquaticKeytarTune : ModProjectile
	{
        int dustType = 59;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquatic Keytar Tune");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.penetrate = 3;
			projectile.timeLeft = 600;
            projectile.extraUpdates = 600;
            projectile.hide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).symphonic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).buffType = mod.BuffType<Buffs.EmpowermentMagicDamage>();
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 6;
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
            projectile.velocity.Normalize();
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