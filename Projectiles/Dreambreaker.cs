using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class Dreambreaker : ModProjectile
	{
        // flail that closely follows mouse and bounces off of enemies, like a heavy yo-yo

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dreambreaker");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 34;
			projectile.height = 34;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 300;
            projectile.netImportant = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>().necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>().melee = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>().lifeSteal = 5;
        }

		public override void AI()
        {
            projectile.rotation += projectile.ai[0];
            projectile.ai[0] += Main.rand.NextFloat(-0.2f, 0.2f);

            Player player = Main.player[projectile.owner];
            if (player.dead)
            {
                projectile.Kill();
            }

            Vector2 toPlayer = player.Center - projectile.Center;
            if (toPlayer.LengthSquared() > 640f * 640f) projectile.Kill();
            if (player.channel)
            {
                projectile.timeLeft = 300;
            }
            else
            {
                projectile.penetrate = -1;
                projectile.velocity = projectile.velocity * 0.95f + toPlayer * 0.05f;
                if (toPlayer.LengthSquared() < 64f * 64f) projectile.Kill();
                projectile.tileCollide = false;
                return;
            }

            if (Main.myPlayer == projectile.owner)
            {
                if (toPlayer.LengthSquared() > 480f * 480f)
                {
                    projectile.velocity = projectile.velocity * 0.97f + toPlayer * 0.05f;
                }
                else
                {
                    Vector2 mouse = Main.MouseWorld;
                    Vector2 toMouse = (mouse - projectile.Center);


                    projectile.velocity = projectile.velocity * 0.95f + toMouse * 0.05f + Main.rand.NextVector2Circular(0.1f, 0.1f);
                    if (projectile.velocity.LengthSquared() > 32f * 32f)
                    {
                        projectile.velocity = projectile.velocity.SafeNormalize(Vector2.Zero) * 32f;
                    }

                    projectile.netUpdate = true;
                }
            }

            Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 57);
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
            projectile.velocity *= 0.5f;

            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 playerCenter = Main.player[projectile.owner].MountedCenter;
            Vector2 center = projectile.Center;
            Vector2 distToProj = playerCenter - projectile.Center;
            float projRotation = distToProj.ToRotation() - 1.57f;
            float distanceSq = distToProj.LengthSquared();
            center -= distToProj.SafeNormalize(Vector2.Zero) * 12f;
            while (distanceSq > 12f * 12f && !float.IsNaN(distanceSq))
            {
                distToProj.Normalize();                 //get unit vector
                distToProj *= 12f;                      //speed = 24
                center += distToProj;                   //update draw position
                distToProj = playerCenter - center;    //update distance
                distanceSq = distToProj.LengthSquared();
                Color drawColor = lightColor;

                //Draw chain
                spriteBatch.Draw(mod.GetTexture("Projectiles/DreambreakerChain"), new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y),
                    new Rectangle(0, 0, 10, 12), drawColor, projRotation,
                    new Vector2(5f, 6f), 1f, SpriteEffects.None, 0f);
            }
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.player[projectile.owner].channel)
            {
                projectile.velocity *= -1.5f;
            }
        }
    }
}