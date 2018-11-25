using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class LivingBladeShot : ModProjectile
	{
        // projectile that homes on very near enemies
        // slows to a stop and waits if there are no nearby enemies
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Living Blade Shot");
        }

        public override void SetDefaults()
        {
            projectile.melee = true;
            projectile.width = 8;
			projectile.height = 8;
            projectile.friendly = true;
            projectile.penetrate = 1;
			projectile.timeLeft = 180;
            projectile.hide = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).glow = true;

        }

		public override void AI()
		{
            projectile.velocity *= 0.95f;
            NPC npc = Necromancy.NearestNPC(projectile.Center, 64f, false, true);
            if (npc != null)
            {
                projectile.velocity += 0.05f * (npc.Center - projectile.Center);
            }

            for (int i = 0; i < 4; i++)
            { 
                Dust d = Dust.QuickDust(projectile.Center + projectile.velocity * i / 4f, new Color(0.5f, 1f, 0f));
                d.noGravity = true;
                d.velocity = projectile.velocity * 0.2f;
            }
        }
    }
}