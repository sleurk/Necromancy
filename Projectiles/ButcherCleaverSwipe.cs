using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Projectiles
{
    public class ButcherCleaverSwipe : ScytheSwipe
    {
        // arkhalis-like projectile, look in ScytheSwipe for actual behavior

        protected override int DustType
        {
            get { return 117; }
        }
        protected override Color Color
        {
            get { return new Color(0.4F, 0.2F, 0.2F); }
        }

        protected override int FrameLength
        {
            get { return 3; }
        }

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
        }
    }
}
