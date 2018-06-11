using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Projectiles
{
    public class ButcherCleaverSwipe : ScytheSwipe
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Butcher's Cleaver Swipe");
            Main.projFrames[mod.ProjectileType("ButcherCleaverSwipe")] = 16;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.width = 60;
            projectile.height = 34;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).lifeSteal = 2;
            dustType = 117;
            r = 0.4f;
            g = 0.2f;
            b = 0.2f;
        }
    }
}
