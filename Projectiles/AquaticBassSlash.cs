using Necromancy.Empowerments;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class AquaticBassSlash : ModProjectile
	{
        // basic projectile ai, moves in a straight line
        readonly int dustType = 246;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquatic Bass Slash");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 24;
			projectile.height = 24;
			projectile.friendly = true;
			projectile.penetrate = 12;
            projectile.timeLeft = 200;
            projectile.extraUpdates = 39;
            projectile.hide = true;
            projectile.tileCollide = false;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).symphonic = true;
            // projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).empowermentType = EmpType.RadiantDamage; NYI
        }

		public override void AI()
        {
            projectile.velocity.Normalize();
            Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, dustType).noGravity = true;
        }
    }
}