using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Necromancy.NPCs;

namespace Necromancy.Projectiles
{
	public class Thunderbolt : ModProjectile
	{
        // charged projectile, when released it shoots towards mouse and homes
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thunderbolt");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            // thorium.radiant = true
            projectile.width = 16;
			projectile.height = 16;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.penetrate = 20;
            ProjectileID.Sets.Homing[projectile.type] = true;
            projectile.timeLeft = 240;
            projectile.hide = true;
            projectile.extraUpdates = 80;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shock = true;
        }

        public override void AI()
        {
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).healPower = (int)(projectile.ai[0] / 3f);
            for (int i = -3; i <= 3; i++)
            {
                Dust.QuickDust(projectile.Center + projectile.velocity.RotatedBy(MathHelper.PiOver2) * i, new Color(1f, 0.8f, 0f)).velocity = projectile.velocity * (5 - Math.Abs(i));
            }

            NPC target = Necromancy.NearestNPC(projectile.Center, 300f, true, true);
            if (target != null)
            {
                float speed = projectile.velocity.Length();
                projectile.velocity = projectile.velocity * 0.99f + (target.Center - projectile.Center) * 0.01f;
                projectile.velocity = projectile.velocity.SafeNormalize(Vector2.Zero) * speed;
            }
        }

        // direct heal or no?
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.GetGlobalNPC<NecromancyNPC>().lightningHit = true;
        }
    }
}