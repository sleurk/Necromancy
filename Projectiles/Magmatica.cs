using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class Magmatica : ModProjectile
	{
        // shot out, upon hitting something, slowly retracts back to player
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magmatica");
        }

        public override void SetDefaults()
        {
            projectile.melee = true;
            projectile.width = 24;
			projectile.height = 34;
			projectile.friendly = true;
			projectile.penetrate = 8;
			projectile.timeLeft = 300;
            projectile.netImportant = true;
            projectile.tileCollide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).burn = true;
        }

		public override void AI()
        {
            Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 6, projectile.velocity.X, projectile.velocity.Y).noGravity = true;
            projectile.rotation = ((projectile.ai[0] == 1 ? -1f : 1f) * projectile.velocity).ToRotation();
            Player player = Main.player[projectile.owner];
            if (player.dead)
            {
                projectile.Kill();
            }

            if (projectile.timeLeft == 280) Bounce();
            if (projectile.ai[0] == 1f)
            {
                Vector2 toPlayer = player.Center - projectile.Center;
                if (toPlayer.Length() > 1200f || toPlayer.Length() < 32f) projectile.Kill();
                projectile.velocity = toPlayer.SafeNormalize(Vector2.Zero) * 16f;
            }
        }

        private void Bounce()
        {
            if (projectile.ai[0] != 1f)
            {
                projectile.velocity *= -1f;
                projectile.ai[0] = 1f;
                projectile.tileCollide = false;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Bounce();
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 playerCenter = Main.player[projectile.owner].MountedCenter;
            Vector2 center = projectile.Center;
            Vector2 distToProj = playerCenter - projectile.Center;
            float projRotation = distToProj.ToRotation() - 1.57f;
            float distance = distToProj.Length();
            center -= distToProj.SafeNormalize(Vector2.Zero) * 12f;
            while (distance > 12f && !float.IsNaN(distance))
            {
                distToProj.Normalize();
                distToProj *= 12f;
                center += distToProj;
                distToProj = playerCenter - center;
                distance = distToProj.Length();
                Color drawColor = lightColor;

                //Draw chain
                spriteBatch.Draw(mod.GetTexture("Projectiles/MagmaticaChain"), new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y),
                    new Rectangle(0, 0, 10, 12), drawColor, projRotation,
                    new Vector2(5f, 6f), 1f, SpriteEffects.None, 0f);
            }
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 5;
            if (projectile.penetrate == 1)
            {
                projectile.penetrate = -1;
                Bounce();
            }
        }
    }
}