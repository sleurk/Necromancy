using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class CryonicTrail : ModProjectile
	{
        // trail left by cryonic javelin, slows to a stop
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cryonic Trail");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = true;
            projectile.tileCollide = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 300;
            projectile.hide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).throwing = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).ice = true;
        }

        public override void AI()
        {
            projectile.velocity *= 0.9f;
            Dust.QuickDust(projectile.Center, new Color(0.05f, 0.9f, 1f));
        }
    }
}