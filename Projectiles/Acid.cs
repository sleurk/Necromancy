using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class Acid : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acid");
        }

        public override void SetDefaults()
        {
            projectile.melee = true;
            projectile.width = 8;
			projectile.height = 8;
            projectile.extraUpdates = 2;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 3600;
            projectile.hide = false;
            projectile.extraUpdates = 3;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).goo = true;
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

        public override bool PreAI()
        {
            Lighting.AddLight(projectile.Center, 0f, 1f, 0f);
            return true;
        }

        public override void AI()
        {
            if (projectile.ai[0] == 0f) projectile.velocity.Y += 0.05f;
            if (projectile.ai[0] == 0f) Dust.QuickDust(projectile.Center + Main.rand.NextVector2Circular(projectile.width / 2, projectile.height / 2), new Color(0f, 1f, 0f));
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Vector2 vel = (projectile.velocity - oldVelocity);
            vel.Normalize();
            projectile.Center -= vel * projectile.height / 2f;
            projectile.ai[0] = 1f;
            projectile.velocity = Vector2.Zero;
            return false;
        }
    }
}