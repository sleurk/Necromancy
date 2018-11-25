using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class Bioluminescence : ModProjectile
	{
        // weird projectile that turns at a changing rate, weapon shoots them to all hit near a point
        public const float ROTATION_RATE = 0.02f;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bioluminescence");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            // thorium.radiant = true;
            projectile.width = 8;
			projectile.height = 8;
            projectile.friendly = true;
            projectile.penetrate = 1;
			projectile.timeLeft = 260;
            projectile.extraUpdates = 12;
            projectile.tileCollide = false;
            projectile.netImportant = true;
            projectile.hide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).glow = true;
        }

		public override void AI()
        {
            bool yellow = projectile.ai[0] == 1f;
            projectile.velocity = projectile.velocity.RotatedBy(ROTATION_RATE * projectile.timeLeft / 200f);
            Dust d = Dust.QuickDust(projectile.Center, yellow ? new Color (1f, 1f, 0f) : new Color(0.5f, 1f, 0f));
            d.velocity = projectile.velocity * 0.5f;
        }
    }
}