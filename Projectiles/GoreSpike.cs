using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
    public class GoreSpike : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gore Spike");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 6;
            projectile.height = 6;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.timeLeft = 60;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            Main.projFrames[projectile.type] = 4;
            aiType = ProjectileID.Bullet;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
        }

        public override void AI()
        {
            projectile.velocity *= 0.95f;
            projectile.frameCounter++;
            if (projectile.frameCounter >= 3)
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 4;
            }
            Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 136, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f).noGravity = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.velocity *= 0.5f;
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 15; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 136, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
            Main.PlaySound(SoundID.Item10, projectile.position);
        }
    }
}
