using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Projectiles
{
	public class PerditionRing : ModProjectile
	{
        // flamelash clone

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Perdition Ring");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 16;
			projectile.height = 16;
			projectile.friendly = true;
			projectile.penetrate = 4;
            projectile.extraUpdates = 3;
			projectile.timeLeft = 1800;
            projectile.hide = true;
            projectile.tileCollide = true;
            projectile.netImportant = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
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
            projectile.scale = projectile.ai[0];

            Player owner = Main.player[projectile.owner];
            projectile.netUpdate = true;
            if (owner.dead || (projectile.ai[0] == 0f) && !owner.channel)
            {
                projectile.ai[0] = 1f;
            }
            if (owner.whoAmI == Main.myPlayer && (projectile.ai[0] == 0f) && owner.channel && !owner.dead)
            {
                Vector2 toMouse = Main.MouseWorld - projectile.Center;
                projectile.velocity = toMouse;
                if (projectile.velocity.LengthSquared() > 4f * 4f) projectile.velocity = projectile.velocity.SafeNormalize(Vector2.Zero) * 4f;
            }

            if (Main.rand.NextFloat() < 0.3f) Dust.QuickDust(projectile.Center + Main.rand.NextVector2CircularEdge(projectile.width / 2f, projectile.height / 2f), new Color(0.2f, 0f, 0.6f));
        }
    }
}