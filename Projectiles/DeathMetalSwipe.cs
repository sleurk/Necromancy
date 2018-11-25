using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Necromancy.Empowerments;

namespace Necromancy.Projectiles
{
	public class DeathMetalSwipe : ScytheSwipe
    {
        // arkhalis-type projectile, look in ScytheSwipe for actual behavior

        protected override int DustType
        {
            get { return 42; }
        }
        protected override Color Color
        {
            get { return new Color(0F, 0.6F, 0.5F); }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Death Metal Swipe");
            Main.projFrames[mod.ProjectileType("DeathMetalSwipe")] = 16;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.width = 134;
            projectile.height = 154;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).melee = false;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).symphonic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).empowermentType = EmpType.AttackSpeed;
        }
    }
}
 