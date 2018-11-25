using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Projectiles
{
    public class DarkDaggerSwipe : ScytheSwipe
    {
        // arkhalis-type projectile, look in ScytheSwipe for actual behavior

        protected override int DustType
        {
            get { return 14; }
        }
        protected override Color Color
        {
            get { return new Color(0.2F, 0.1F, 0.3F); }
        }

        protected override int FrameLength
        {
            get { return 3; }
        }

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
        }
    }
}
