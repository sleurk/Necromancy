using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace Necromancy.Projectiles
{
	public class BloodyMirrorCircle : ModProjectile
	{
        // large projectile that stays on player
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloody Mirror Circle");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 204;
			projectile.height = 204;
			projectile.friendly = true;
            projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 1200;
            projectile.alpha = 100;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
        }

		public override void AI()
		{
            projectile.Center = Main.player[projectile.owner].Center;
            projectile.rotation += 0.05f;
            if (Main.player[projectile.owner].dead)
            {
                projectile.Kill();
            }
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