using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Projectiles
{
    public class DarkDaggerSwipe : ScytheSwipe
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Dagger Swipe");
            Main.projFrames[mod.ProjectileType("DarkDaggerSwipe")] = 16;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.width = 60;
            projectile.height = 34;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).lifeSteal = 2;
            dustType = 14;
            r = 0.2f;
            g = 0.1f;
            b = 0.3f;
        }
    }
}
