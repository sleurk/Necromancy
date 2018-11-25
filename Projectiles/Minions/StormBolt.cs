using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles.Minions
{
    public class StormBolt : ModProjectile
    {
        // shot by sentry summon, basic projectile
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Storm Bolt");
        }

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 300;
            projectile.hide = true;
            projectile.extraUpdates = 100;
            ProjectileID.Sets.Homing[projectile.type] = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shock = true;

        }

        public override void AI()
        {
            for (int i = -1; i <= 1; i++)
            {
                float speed = projectile.timeLeft / 300f;
                Dust.QuickDust(projectile.Center + projectile.velocity.RotatedBy(MathHelper.PiOver2) * i * speed, new Color(1f, 0.8f, 0.2f)).velocity = projectile.velocity * speed * Main.rand.NextFloat(0f, 2f);
            }
        }
    }
}