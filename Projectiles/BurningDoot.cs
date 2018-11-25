using Necromancy.Empowerments;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Necromancy.NPCs;

namespace Necromancy.Projectiles
{
	public class BurningDoot : ModProjectile
    {
        // homing laser projectile
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Burning Doot");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            // thorium.symphonic = true
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 240;
            projectile.extraUpdates = 60;
            projectile.hide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).burn = true;
        }

        public override void AI()
        {
            Dust.QuickDust(projectile.Center, new Color(1f, 0.5f, 0f));

            NPC target = Necromancy.NearestNPC(projectile.Center + projectile.velocity * 4f, 240f, false, true);
            if (target != null)
            {
                float speed = projectile.velocity.Length();
                projectile.velocity = 0.995f * projectile.velocity + 0.005f * (target.Center - projectile.Center);
                projectile.velocity = projectile.velocity.SafeNormalize(Vector2.Zero) * speed;
            }
        }
    }
}