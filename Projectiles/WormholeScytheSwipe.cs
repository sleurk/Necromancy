using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Projectiles
{
	public class WormholeScytheSwipe : ScytheSwipe
    {
        // arkhalis-type projectile, see ScytheSwipe for actual behavior

        protected override int DustType
        {
            get { return 27; }
        }
        protected override Color Color
        {
            get { return new Color(0.5F, 0.36F, 1F); }
        }

        protected override int FrameLength
        {
            get { return 2; }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wormhole Reaper Swipe");
            Main.projFrames[mod.ProjectileType("WormholeScytheSwipe")] = 16;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.alpha = 50;
            projectile.width = 120;
            projectile.height = 138;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).lifeSteal = 7;
        }
    }
}
 