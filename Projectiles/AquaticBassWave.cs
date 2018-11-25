using Necromancy.Empowerments;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class AquaticBassWave : ModProjectile
	{
        // wavy projectile, vertical sine wave
        readonly int dustType = 135;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquatic Bass Wave"); 
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.penetrate = 3;
            projectile.timeLeft = 360;
            projectile.extraUpdates = 5;
            projectile.hide = true;
            projectile.extraUpdates = 2;
            projectile.tileCollide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).symphonic = true;
            // projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).empowermentType = EmpType.SymphonicDamage; NYI
        }

		public override void AI()
        {
            projectile.velocity.Y += (float)Math.Cos(MathHelper.ToRadians(projectile.timeLeft * 16));
            Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, dustType, projectile.velocity.X, projectile.velocity.Y).noGravity = true;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 6;
        }
    }
}