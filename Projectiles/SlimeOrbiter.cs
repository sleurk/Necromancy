using Necromancy.Empowerments;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class SlimeOrbiter : ModProjectile
	{
        // orbits around the nearest SlimeCenter
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slime Orbiter");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 1200;
            projectile.hide = true;
            projectile.tileCollide = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 30;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).goo = true;
        }

		public override void AI()
        {
            Projectile orbiting = null;
            float minDist = -1;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == mod.ProjectileType("SlimeCenter") && Main.projectile[i].owner == projectile.owner 
                 && (Main.projectile[i].Center - projectile.Center).Length() < 600f && (minDist == -1 || minDist > (Main.projectile[i].Center - projectile.Center).Length()))
                {
                    orbiting = Main.projectile[i];
                    minDist = (Main.projectile[i].Center - projectile.Center).Length();
                }
            }
            if (orbiting != null)
            {
                Vector2 toTarget = orbiting.Center - projectile.Center;
                if (toTarget.Length() < 600f)
                {
                    projectile.velocity += toTarget * 0.01f;
                }
            }
            else projectile.Kill();
            Dust.QuickDust(projectile.Center, new Color(0f, 0.6f, 0f)).velocity = projectile.velocity;
		}
    }
}