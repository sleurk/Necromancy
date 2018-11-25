using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Projectiles.Minions
{
    public class SpiritFlame : ModProjectile
    {
        // projectile shot by Spirit, blue fire projectile
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirit Flame");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.extraUpdates = 1;
            projectile.timeLeft = 30;
            projectile.hide = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).summon = true;
        }

        public override void AI()
        {
            projectile.velocity *= 0.95f;
            Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 135, projectile.velocity.X, projectile.velocity.Y, 0, default(Color), 2f);
            d.noGravity = true;
        }
    }
}