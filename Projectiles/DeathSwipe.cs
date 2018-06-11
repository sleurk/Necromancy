using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Projectiles
{
	public class DeathSwipe : ScytheSwipe
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Death Swipe");
            Main.projFrames[mod.ProjectileType("DeathSwipe")] = 16;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.width = 180;
            projectile.height = 206;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).lifeSteal = 8;
            dustType = 54;
            r = 0.3f;
            g = 0f;
            b = 0f;
        }
    }
}
 