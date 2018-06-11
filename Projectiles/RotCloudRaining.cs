using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
    public class RotCloudRaining : ModProjectile
    {
        private bool set;
        private bool disappear;
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
            set = true;
            disappear = false;
        }

        public override void AI()
        {
            if (set)
            {
                set = false;
                Main.player[projectile.owner].GetModPlayer<NecromancyPlayer>(mod).rotCloud = projectile;
            }

            if (!disappear && Main.player[projectile.owner].GetModPlayer<NecromancyPlayer>(mod).rotCloud != projectile)
            {
                projectile.timeLeft = 10;
                disappear = true;
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
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom;
            }
        }
    }
}
