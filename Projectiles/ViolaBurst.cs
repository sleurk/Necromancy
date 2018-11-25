using Necromancy.Empowerments;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class ViolaBurst : ModProjectile
	{
        // basic projectile, when hitting an enemy it shoots another projectile towards the hit enemy
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Viola Burst");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            // thorium.symphonic = true
            projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
            projectile.extraUpdates = 1;
            projectile.hide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).glow = true;
        }

		public override void AI()
        {
            for (int i = 0; i < 3; i++)
            {
                Dust.QuickDust(projectile.Center + projectile.velocity * i / 3f, projectile.ai[0] == 1f ? new Color(1f, 1f, 0f) : new Color(0.5f, 1f, 0f)).velocity = projectile.velocity;
            }
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[0] < 5f)
            {
                Player player = Main.player[projectile.owner];
                Projectile proj = Projectile.NewProjectileDirect(player.Center, (target.Center - player.Center).SafeNormalize(Vector2.Zero) * 1.2f * projectile.velocity.Length(),
                    projectile.type, projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0] + 1f);
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom;
            }
        }
    }
}