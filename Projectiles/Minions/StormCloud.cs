using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles.Minions
{
	public class StormCloud : ModProjectile
	{
        // NYI - summons are weird
        // shoots lightning at all enemies below it

        private int timer = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Storm Cloud");
        }

        public override void SetDefaults()
        {
            projectile.sentry = true;
            projectile.netImportant = true;
			projectile.width = 178;
			projectile.height = 64;
            Main.projFrames[projectile.type] = 6;
			projectile.friendly = false;
			projectile.penetrate = -1;
			projectile.timeLeft = Projectile.SentryLifeTime;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
        }

		public override void AI()
		{
            timer = (timer + 3) % 360;
            projectile.velocity = new Vector2(0f, (float)Math.Sin(MathHelper.ToRadians(timer))) / 3f;
            projectile.frameCounter = (projectile.frameCounter + 1) % 5;
            if (projectile.frameCounter == 0)
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 6;
                if (projectile.frame == 3)
                {
                    Shoot();
                }
            }
            if (!Main.player[projectile.owner].active || Main.player[projectile.owner].dead) projectile.Kill();
        }

        private void Shoot()
        {
            Vector2 origin = projectile.Center + new Vector2(0f, 20f);
            NPC[] npcs = Necromancy.NearbyNPCs(origin, 600f, true);
            int max = Math.Min(npcs.Length, 5);
            for (int i = 0; i < max; i++)
            {
                NPC target = npcs[i];
                if (target.Center.Y < origin.Y)
                {
                    max = Math.Min(npcs.Length, max + 1);
                }
                else if (target != null)
                {
                    Vector2 toTarget = (target.Center - origin).SafeNormalize(Vector2.Zero) * 4;
                    Projectile proj = Projectile.NewProjectileDirect(origin, toTarget, mod.ProjectileType("StormBolt"), projectile.damage, projectile.knockBack, projectile.owner);
                    proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = projectile.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom;
                }
            }
        }
    }
}