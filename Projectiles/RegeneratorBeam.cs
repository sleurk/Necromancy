using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Projectiles
{
	public class RegeneratorBeam : ModProjectile
	{
        // shoots in a weird spread and has weird visuals but it's pretty normal otherwise
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Regenerator Beam");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 4;
			projectile.height = 4;
            projectile.friendly = true;
            projectile.penetrate = 1;
			projectile.timeLeft = 300;
            projectile.hide = true;
            projectile.netImportant = true;
            projectile.extraUpdates = 25;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).radiant = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).healPower = 8;
        }

		public override void AI()
        {
            projectile.velocity = projectile.velocity.RotatedBy(projectile.ai[0] * 0.005f);
            for (int i = 0; i < projectile.velocity.Length(); i += 8)
            {
                Dust d = Dust.QuickDust(projectile.position + projectile.velocity.SafeNormalize(Vector2.Zero) * i, new Color(1f, 1f, 0.5f));
                d.velocity = 0.08f * (Main.player[projectile.owner].Center - d.position);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust d = Dust.NewDustDirect(target.position, target.width, target.height, 64, 0f, 0f, 100, default(Color), 3f);
                d.velocity = 0.08f * (Main.player[projectile.owner].Center - d.position);
                d.noGravity = true;
                d = Dust.NewDustDirect(target.position, target.width, target.height, 61, 0f, 0f, 100, default(Color), 3f);
                d.velocity = 0.08f * (Main.player[projectile.owner].Center - d.position);
                d.noGravity = true;
            }
        }
    }
}