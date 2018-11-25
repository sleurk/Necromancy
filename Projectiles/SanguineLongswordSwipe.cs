using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Projectiles
{
	public class SanguineLongswordSwipe : ScytheSwipe
    {
        // arkhalis-like projectile, look in ScytheSwipe for actual behavior

        protected override int DustType
        {
            get { return 5; }
        }
        protected override Color Color
        {
            get { return new Color(0.9F, 0F, 0F); }
        }

        protected override int FrameLength
        {
            get { return 2; }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sanguine Longsword Swipe");
            Main.projFrames[mod.ProjectileType("SanguineLongswordSwipe")] = 16;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.melee = true;
            projectile.magic = false;
            projectile.width = 72;
            projectile.height = 154;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = false;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).melee = false;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).blood = true;
        }
    }
}
 