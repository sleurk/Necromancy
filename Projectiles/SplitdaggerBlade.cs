using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
    public class SplitdaggerBlade : ModProjectile
    {
        // basic short-range projectile created when Splitdagger splits
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Splitdagger");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 10;
            projectile.height = 18;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.timeLeft = 15;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).throwing = true;
        }

        public override void AI()
        {
            Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 14, projectile.velocity.X, projectile.velocity.Y);
            d.scale = 0.6f;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
            {
                Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 14, projectile.velocity.X, projectile.velocity.Y);
                d.scale = 0.8f;
            }
        }
    }
}
