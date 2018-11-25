using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class Ooze : ModProjectile
	{
        // basic projectile, loosely sticks to enemies
        // every hit it matches the target's velocity
        // enemies moving in a semi-straight line will be hit multiple times
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ooze");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            // thorium.radiant = true
            projectile.width = 32;
			projectile.height = 32;
			projectile.friendly = true;
			projectile.penetrate = 20;
            projectile.hide = true;
            projectile.timeLeft = 600;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).goo = true;
        }

		public override void AI()
		{
            for (int k = 0; k < 10; k++)
            {
                Dust d = Dust.QuickDust(projectile.Center + Main.rand.NextVector2Circular(projectile.width / 2, projectile.height / 2), new Color(Main.rand.NextFloat(0.2f), 1f, Main.rand.NextFloat(0.2f)));
                d.velocity = 0.2f * (d.position - projectile.Center);
                d.velocity /= (float)Math.Sqrt(d.velocity.Length());
            }
        }

        public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 20; k++)
			{
				Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 61, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
			}
		}

        // direct heal or no?
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.velocity = target.velocity;
            if (projectile.velocity.Length() > 32f) projectile.velocity = projectile.velocity.SafeNormalize(Vector2.Zero) * 32f;
            target.AddBuff(mod.BuffType("Goo"), 300);
        }
    }
}