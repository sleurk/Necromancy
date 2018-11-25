using Necromancy.Empowerments;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class ChillingBreath : ModProjectile
	{
        // moves along a set path and reverses direction every 80 frames
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chilling Breath");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            // thorium.symphonic = true
            projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 1600;
            projectile.hide = true;
            projectile.tileCollide = false;
            projectile.extraUpdates = 1;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).ice = true;
        }


        public override void AI()
        {
            if (projectile.timeLeft % 80 == 1) projectile.velocity *= -1f;

            Dust.QuickDust(projectile.Center, new Color(0f, Main.rand.NextFloat(0.5f, 1f), 1f)).velocity = projectile.velocity * 0.25f + Main.rand.NextVector2Circular(0.5f, 0.5f);
		}
    }
}