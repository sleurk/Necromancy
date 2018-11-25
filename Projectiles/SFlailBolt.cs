using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Projectiles
{
	public class SFlailBolt : ElectricBolt
    {
        // electric bolt from static flail
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Static Flail Bolt");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.melee = true;
        }
    }
}