using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class WormholeScytheShot : ModProjectile
	{
        // shot by WormholeScythe, sticks to enemies to continue sapping life over time

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wormhole");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 8;
			projectile.height = 8;
            projectile.aiStyle = 0;
			projectile.friendly = true;
            projectile.tileCollide = true;
			projectile.penetrate = -1;
            projectile.extraUpdates = 2;
			projectile.timeLeft = 90;
            projectile.hide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).melee = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).lifeSteal = 7;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 45;
        }

        public override void AI()
        {
            NPC stuckTo = Necromancy.NearestNPC(projectile.Center, 64f, false, false);
            if (stuckTo != null)
            {
                projectile.Center = stuckTo.Center;
                projectile.velocity = Vector2.Zero;
                Dust d = Dust.QuickDust(projectile.Center + Main.rand.NextVector2CircularEdge(24f, 24f), new Color(0.5f, 0.36f, 1f));
                d.velocity = (stuckTo.Center - d.position) * 0.2f;
                d.velocity = d.velocity.RotatedBy(Main.rand.NextFloat(0.1f, 0.4f)) + stuckTo.velocity;
                if (!stuckTo.active) projectile.Kill();
            }
            else
            {
                Dust.QuickDust(projectile.Center, new Color(0.5f, 0.36f, 1f)).velocity = projectile.velocity * 0.4f;
                Dust.QuickDust(projectile.Center + projectile.velocity / 2f, new Color(0.5f, 0.36f, 1f)).velocity = projectile.velocity * 0.4f;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.timeLeft = 90;
        }
    }
}