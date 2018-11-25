using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Projectiles
{
	public class PHarmBolt : ElectricBolt
    {
        // electric bolt from pylons
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pylon Bolt");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.magic = true;
        }
    }
}