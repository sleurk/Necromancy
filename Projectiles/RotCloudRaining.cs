using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
    public class RotCloudRaining : ModProjectile
    {
        // nimbus rod clone, cloud while in position and raining
        private const int NUM_CLOUDS = 1;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rot Cloud");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 54;
            projectile.height = 28;
            projectile.friendly = false;
            projectile.hostile = false;
            projectile.timeLeft = 3600;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.netImportant = true;
            projectile.penetrate = -1;
            Main.projFrames[projectile.type] = 6;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
        }

        public override void AI()
        {
            if (projectile.ai[0] == 0f)
            {
                projectile.ai[0] = 1f;
                List<Projectile> clouds = new List<Projectile>();
                foreach (Projectile proj in Main.projectile)
                {
                    if (proj != null && proj.active && proj.type == projectile.type && proj.owner == projectile.owner)
                    {
                        clouds.Add(proj);
                    }
                }
                clouds.Sort(new ProjectileAgeComparer());
                for (int i = clouds.Count - 1; i >= NUM_CLOUDS; i--)
                {
                    clouds[i].Kill();
                }
            }

            if (projectile.timeLeft < 10)
            {
                projectile.alpha += 25;
            }

            projectile.frameCounter++;
            if (projectile.frameCounter >= 7)
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 6;
            }

            if (projectile.timeLeft % 10 == 0)
            {
                Vector2 pos = new Vector2(Main.rand.NextFloat(projectile.position.X + projectile.width * 1/4f, projectile.position.X + projectile.width * 3/4f), projectile.Center.Y);
                Projectile proj = Projectile.NewProjectileDirect(pos, new Vector2(0, 10f), mod.ProjectileType("RotRain"), projectile.damage, projectile.knockBack, projectile.owner);
                proj.position = pos;
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom;
            }
        }
    }
}
