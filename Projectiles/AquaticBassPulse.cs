using Microsoft.Xna.Framework;
using Necromancy.Empowerments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class AquaticBassPulse : ModProjectile
    {
        // short-range projectile with modular gravity in projectile.ai[0]
        readonly int dustType = 127;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquatic Bass Pulse");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.penetrate = 1;
            projectile.timeLeft = 400;
            projectile.hide = true;
            projectile.extraUpdates = 10;
            projectile.tileCollide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).symphonic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).empowermentType = EmpType.ThrowingDamage;
        }

		public override void AI()
        {
            projectile.velocity.Y += 0.004f * projectile.ai[0];
            Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, dustType).noGravity = true;
		}
    }
}