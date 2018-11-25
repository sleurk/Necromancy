using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
    public class Dice4Shot : ModProjectile
    {
        // projectile created by throwing dice on a '4'
        // bounces on walls
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dice 4 Shot");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 8;
            projectile.height = 8;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.penetrate = -1;
            projectile.extraUpdates = 4;
            projectile.timeLeft = 1200;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).throwing = true;
        }

        public override void AI()
        {
            Dust.QuickDust(projectile.Center, Color.Red);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }
    }
}