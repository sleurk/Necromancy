using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Projectiles
{
	public class DeathSwipe : ScytheSwipe
    {
        // arkhalis-type projecitle, look in ScytheSwipe for actual behavior

        protected override int DustType
        {
            get { return 54; }
        }
        protected override Color Color
        {
            get { return new Color(0.3F, 0F, 0F); }
        }

        protected override int FrameLength
        {
            get { return 1; }
        }

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
        }
    }
}
 