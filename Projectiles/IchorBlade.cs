using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class IchorBlade : ModProjectile
	{
        // slow, spinning projectile that moves towards cursor
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ichor Blade");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 40;
			projectile.height = 40;
			projectile.friendly = true;
			projectile.penetrate = 3;
			projectile.timeLeft = 600;
            projectile.netImportant = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).melee = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).ichor = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).lifeSteal = 3;
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

        public override void AI()
        {
            projectile.netUpdate = true;
            if (projectile.owner == Main.myPlayer)
            {
                Vector2 toMouse = Main.MouseWorld - projectile.Center;
                toMouse.Normalize();
                projectile.velocity = toMouse * 1.5f;
            }
            projectile.rotation += 0.3f * projectile.ai[1];
            Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 246, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 246, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
        }

        public override bool PreAI()
        {
            Lighting.AddLight(projectile.position, .6f, .6f, 0f);
            return true;
        }
    }
}