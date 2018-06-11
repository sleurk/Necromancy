using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Projectiles
{
	public class SoulScytheSwipe : ScytheSwipe
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Scythe Swipe");
            Main.projFrames[mod.ProjectileType("SoulScytheSwipe")] = 16;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.width = 90;
            projectile.height = 80;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).lifeSteal = 3;
            dustType = 135;
            r = 0f;
            g = 0.6f;
            b = 0.8f;
        }
    }
}
 