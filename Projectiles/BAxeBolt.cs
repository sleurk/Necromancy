using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Projectiles
{
	public class BAxeBolt : ElectricBolt
    {
        // lightning from bolt axes
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Axe Bolt");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.thrown = true;
        }
    }
}