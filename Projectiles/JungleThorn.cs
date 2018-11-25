using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
    public class JungleThorn : ModProjectile
    {
        // weird projectile that splits erratically at right angles
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jungle Thorn");
        }

        public override void SetDefaults()
        {
            projectile.thrown = true;
            projectile.width = 14;
            projectile.height = 22;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.timeLeft = 120;
            projectile.ignoreWater = true;
            projectile.netImportant = true;
            projectile.extraUpdates = 4;
            projectile.penetrate = -1;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).glow = true;
        }

        public override void AI()
        {
            projectile.position -= projectile.velocity * 0.9f;
            projectile.alpha = (45 - projectile.timeLeft) * 100 / 45;

            if (Main.rand.NextFloat() < 0.1f)
            {
                Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 38, projectile.velocity.X, projectile.velocity.Y);
                d.scale = 0.8f;
                d.noGravity = true;
            }

            projectile.rotation = projectile.velocity.RotatedBy(MathHelper.PiOver2).ToRotation();
            if (projectile.ai[0] > 0 && Main.myPlayer == projectile.owner && projectile.timeLeft % 5 == 0)
            {
                if (projectile.timeLeft == 115)
                {
                    ShootProjectile(0);
                    if (Main.rand.NextFloat() < projectile.ai[0] / 50f)
                    {
                        int direction = Main.rand.Next(2) * 2 - 1;
                        projectile.ai[0] -= 5f;
                        if (direction == Math.Sign(projectile.ai[1])) direction *= -1;
                        ShootProjectile(direction);
                        projectile.ai[0] -= 5f;
                    }
                }
            }
        }

        private void ShootProjectile(int direction)
        {
            float rotate = MathHelper.PiOver2 * direction;
            Vector2 vel = projectile.velocity.RotatedBy(rotate);
            Projectile proj = Projectile.NewProjectileDirect(projectile.Center + vel * 16f, vel, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0] - Main.rand.Next(1, 3), projectile.ai[1] + direction);
            proj.netUpdate = true;
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = projectile.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 6;
        }
    }
}
