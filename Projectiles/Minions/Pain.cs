using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Projectiles.Minions
{
    public class Pain : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pain");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.hide = true;
            projectile.extraUpdates = 25;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).summon = true;
        }

        public override void AI()
        {
            // code from vanilla
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] > 2f)
            {
                int num3;
                for (int num452 = 0; num452 < 4; num452 = num3 + 1)
                {
                    Vector2 vector36 = projectile.position;
                    vector36 -= projectile.velocity * ((float)num452 * 0.25f);
                    projectile.alpha = 255;

                    int num453 = Dust.NewDust(vector36, 1, 1, 229);
                    Main.dust[num453].position = vector36;
                    Main.dust[num453].noGravity = true;
                    Main.dust[num453].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                    Dust dust3 = Main.dust[num453];
                    dust3.velocity *= 0.2f;
                    num3 = num452;
                }
            }
        }
    }
}