using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
    public class GhoulPortal : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ghoul Portal");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 64;
            projectile.height = 64;
            projectile.friendly = true;
            projectile.netImportant = true;
            projectile.tileCollide = false;
            projectile.hide = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 600;
            projectile.alpha = 100;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
        }

        public override void AI()
        {
            projectile.velocity *= 0.99f;
            NPC target = Necromancy.NearestNPC(projectile.Center, 300f);
            if (target != null)
            {
                projectile.velocity += (target.Center - projectile.Center) * 0.01f;
            }
            float maxSpeed = projectile.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom.shootSpeed;
            if (projectile.velocity.Length() > maxSpeed)
            {
                projectile.velocity = projectile.velocity.SafeNormalize(Vector2.Zero) * maxSpeed;
            }
            for (int i = 0; i < 3; i++)
            {
                Vector2 pos = projectile.Center + Main.rand.NextVector2Circular(32f, 32f);
                Dust d = Dust.QuickDust(pos, Main.rand.NextBool() ? Color.Purple : Color.Pink);
                d.velocity = (projectile.Center - d.position).RotatedBy(MathHelper.ToRadians(90));
                d.velocity /= 10f;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 5;
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Vector2 pos = projectile.Center + Main.rand.NextVector2Circular(32f, 32f);
                Dust d = Dust.QuickDust(pos, Main.rand.NextBool() ? Color.Purple : Color.Pink);
                d.velocity = (projectile.Center - d.position).RotatedBy(MathHelper.ToRadians(90));
                d.velocity /= 10f;
            }
        }
    }
}