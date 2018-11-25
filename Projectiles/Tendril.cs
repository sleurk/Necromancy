using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Projectiles
{
	public class Tendril : ModProjectile
	{
        // curvy laser shot from TendrilCluster at nearby enemies

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tendril");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 4;
			projectile.height = 4;
            projectile.friendly = true;
            projectile.penetrate = 1;
			projectile.timeLeft = 360;
            projectile.hide = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            projectile.extraUpdates = 80;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).radiant = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).healPower = 3;
        }

        public override void AI()
        {
            Vector2 targetPos = new Vector2(projectile.ai[0], projectile.ai[1]);
            projectile.velocity = projectile.velocity * 0.999f + (targetPos - projectile.Center) * 0.001f;
            projectile.velocity.Normalize();

            Dust d = Dust.NewDustDirect(projectile.Center, 1, 1, 135);
        }
    }
}