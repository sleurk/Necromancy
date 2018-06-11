using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Projectiles
{
	public class WormholeScytheSwipe : ScytheSwipe
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wormhole Reaper Swipe");
            Main.projFrames[mod.ProjectileType("WormholeScytheSwipe")] = 16;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.width = 120;
            projectile.height = 138;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).lifeSteal = 5;
            dustType = 60;
            r = 0.8f;
            g = 0.1f;
            b = 0.2f;
        }
    }
}
 