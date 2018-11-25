using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class Gunk : ModProjectile
	{
        // short-range projectile, created from Gunk Capsule
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gunk");
        }

        public override void SetDefaults()
        {
            projectile.thrown = true;
            projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 30;
            projectile.hide = false;
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
            Dust.QuickDust(projectile.Center + Main.rand.NextVector2Circular(projectile.width / 2, projectile.height / 2), new Color(0f, 1f, 0f));
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Goo"), 300);
        }
    }
}