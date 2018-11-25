using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class Chloroflame : ModProjectile
	{
        // homing flamethrower projectile
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chloroflame");
        }

        public override void SetDefaults()
        {
            projectile.ranged = true;
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;
            ProjectileID.Sets.Homing[projectile.type] = true;
            projectile.timeLeft = 60;
            projectile.extraUpdates = 5;
            projectile.hide = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).glow = true;
        }

		public override void AI()
		{
            NPC target = Necromancy.NearestNPC(projectile.Center, 160f, false, true);
            if (target != null)
            {
                float speed = projectile.velocity.Length();
                projectile.velocity += (target.Center - projectile.Center).SafeNormalize(Vector2.Zero) * 0.5f;
                projectile.velocity = projectile.velocity.SafeNormalize(Vector2.Zero) * speed;
            }
            Dust d = Dust.QuickDust(projectile.Center + Main.rand.NextVector2Circular(projectile.width / 2, projectile.height / 2) + projectile.velocity * Main.rand.NextFloat(), new Color(Main.rand.NextFloat(0.5f), 1f, 0f));
            d.scale *= 2.0f;
            d.velocity = Main.rand.NextVector2Circular(1f, 1f);
		}
    }
}