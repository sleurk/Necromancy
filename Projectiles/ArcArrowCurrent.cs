using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Projectiles
{
	public class ArcArrowCurrent : ModProjectile
    {
        // lightning from arc arrows
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arc Bow Bolt");
        }

        public override void SetDefaults()
        {
            projectile.ranged = true;
            projectile.width = 8;
            projectile.height = 8;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.penetrate = 1;
            projectile.timeLeft = 60;
            projectile.extraUpdates = 20;
            projectile.aiStyle = 0;
        }

        public override void AI()
        {
            Dust.QuickDust(projectile.Center, new Color(1f, 0.9f, 0.3f)).velocity = projectile.velocity * 0.5f;
        }
    }
}