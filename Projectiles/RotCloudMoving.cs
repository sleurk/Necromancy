using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
    public class RotCloudMoving : ModProjectile
    {
        // nimbus rod clone, cloud while moving to target
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
            projectile.timeLeft = 120;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.netImportant = true;
            projectile.penetrate = -1;
            Main.projFrames[projectile.type] = 4;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
        }

        public override void AI()
        {
            Vector2 target = new Vector2(projectile.ai[0], projectile.ai[1]);

            Rectangle targetRectangle = new Rectangle((int)target.X - 4, (int)target.Y - 4, 8, 8);
            if (projectile.Hitbox.Intersects(targetRectangle))
            {
                projectile.Kill();
                return;
            }
            Vector2 targetDirection = new Vector2(target.X, target.Y) - projectile.Center;
            projectile.velocity = Vector2.Normalize(targetDirection) * 16f;
        }

        public override void Kill(int timeLeft)
        {
            projectile.netUpdate = true;
            Vector2 startPos = new Vector2(projectile.position.X - 8, projectile.position.Y + 24);
            Projectile proj = Projectile.NewProjectileDirect(startPos, new Vector2(0, 0), mod.ProjectileType("RotCloudRaining"), projectile.damage, projectile.knockBack, projectile.owner);
            proj.netUpdate = true;
            proj.Center = new Vector2(projectile.ai[0], projectile.ai[1]);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom;
        }
    }
}
