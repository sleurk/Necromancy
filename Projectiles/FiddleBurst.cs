using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class FiddleBurst : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fiddle Burst");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
            projectile.extraUpdates = 1;
            projectile.hide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).symphonic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).buffType = mod.BuffType<Buffs.EmpowermentCritChance>();
        }

		public override void AI()
        {
            for (int i = 0; i < 3; i++)
            {
                Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 6, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f).noGravity = true;
            }
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 120);
        }

        public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 15; k++)
			{
				Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 6).noGravity = true;
			}
		}
    }
}