using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class ChannelerShot : ModProjectile
	{
        // boomerang projectile, shot with the spear

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Channeler Shot");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 14;
			projectile.height = 28;
			projectile.friendly = true;
			projectile.penetrate = 3;
			projectile.timeLeft = 80;
            projectile.aiStyle = 0;
            projectile.extraUpdates = 1;
            projectile.tileCollide = false;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).melee = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).lifeSteal = 3;
        }

		public override void AI()
        {
            projectile.rotation = projectile.velocity.RotatedBy(MathHelper.PiOver2).ToRotation();

            if (projectile.ai[0] == 1f)
            {
                Vector2 toPlayer = Main.player[projectile.owner].Center - projectile.Center;
                if (toPlayer.Length() < 32f)
                {
                    projectile.Kill();
                }
                toPlayer.Normalize();
                projectile.velocity = projectile.velocity * 0.95f + toPlayer;
            }
            else
            {
                if (projectile.timeLeft < 60)
                {
                    projectile.ai[0] = 1f;
                }
            }
            Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 57, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f).noGravity = true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}