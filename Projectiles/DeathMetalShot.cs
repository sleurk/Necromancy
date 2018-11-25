using Necromancy.Empowerments;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class DeathMetalShot : ModProjectile
	{
        // basic projectile, knocks enemies towards the player because of how it is shot

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Death Metal Shot");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 24;
			projectile.height = 24;
			projectile.friendly = true;
			projectile.penetrate = 1;
            projectile.timeLeft = 600;
            projectile.extraUpdates = 59;
            projectile.hide = true;
            projectile.tileCollide = false;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).symphonic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).empowermentType = EmpType.AttackSpeed;
        }

		public override void AI()
        {
            projectile.velocity.Normalize();
            Dust.QuickDust(projectile.Center + Main.rand.NextVector2Circular(projectile.width / 2 * (projectile.ai[0] - projectile.timeLeft) / projectile.ai[0], 
                projectile.height / 2 * (projectile.ai[0] - projectile.timeLeft) / projectile.ai[0]), new Color(0.6f, 1f, 0.9f)).velocity += projectile.velocity * Main.rand.NextFloat(10f);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Necromancy.DoCustomKnockback(target, projectile.velocity * 12f);
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 9; i++)
            {
                Dust.QuickDust(projectile.Center + Main.rand.NextVector2Circular(projectile.width / 2, projectile.height / 2), new Color(0.6f, 1f, 0.9f)).velocity += projectile.velocity.RotatedByRandom(MathHelper.ToRadians(40)) * Main.rand.NextFloat(10f);
            }
        }
    }
}