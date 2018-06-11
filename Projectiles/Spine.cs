using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
    public class Spine : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spine");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 42;
            projectile.height = 28;
            projectile.aiStyle = 4;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.timeLeft = 60;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.netImportant = true;
            projectile.penetrate = -1;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
        }
        
        public override void AI()
        {
            if (projectile.timeLeft == 57)
            {
                if (projectile.owner == Main.myPlayer)
                {
                    Vector2 vel = projectile.velocity * 0.8f + (Main.MouseWorld - projectile.Center).SafeNormalize(Vector2.Zero) * 0.2f;
                    vel = vel.SafeNormalize(Vector2.Zero) * projectile.velocity.Length();
                    Projectile proj = Projectile.NewProjectileDirect(projectile.Center + vel * ((projectile.ai[0] > 13) ? 28f : 17f), vel, (projectile.ai[0] > 13) ? mod.ProjectileType("SpineTip") : projectile.type, projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0] + 1f);
                    proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom;
                    proj.netUpdate = true;
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 10; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 53, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}