using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
    public class GlacialSpike : ModProjectile
    {
        // basic projectile, slows to a stop
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glacial Spike");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 14;
            projectile.height = 14;
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
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).ice = true;
        }

        public override void AI()
        {
            projectile.velocity *= 0.97f;
            projectile.frameCounter++;
            if (projectile.frameCounter >= 3)
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 4;
            }
            Dust.QuickDust(projectile.Center, new Color(0f, 0.8f, 1f)).velocity = projectile.velocity * Main.rand.NextFloat(0.8f, 1.1f) + Main.rand.NextVector2Circular(1f, 1f);
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 8; k++)
            {
                Dust.QuickDust(projectile.Center, new Color(0f, 0.8f, 1f)).velocity = Main.rand.NextVector2Circular(4f, 4f) + projectile.velocity * Main.rand.NextFloat(0.8f, 1.1f);
            }
            Main.PlaySound(SoundID.Item27, projectile.position);
        }
    }
}
