using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class BlooderbussShot : ModProjectile
	{
        // one large projectile that lasts for its animation then dies
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blooderbuss Shot");
        }

        public override void SetDefaults()
        {
            projectile.ranged = true;
            projectile.width = 176;
			projectile.height = 218;
			projectile.friendly = true;
            projectile.aiStyle = 0;
			projectile.penetrate = -1;
			projectile.timeLeft = 16;
            Main.projFrames[mod.ProjectileType("BlooderbussShot")] = 8;
            projectile.tileCollide = false;
        }

        public override bool? CanHitNPC(NPC target)
        {
            float rotation = projectile.velocity.ToRotation();
            Vector2 relative = (target.Center - projectile.Center).RotatedBy(-rotation);

            if (Math.Abs(relative.Y) >= relative.X + projectile.width / 2f) return false;
            return base.CanHitNPC(target);
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation();
            Lighting.AddLight(projectile.position, 0f, 0.3f, 0.5f);
            if (projectile.timeLeft % 2 == 0) projectile.frame++;
            Vector2 dustVel = projectile.velocity.RotatedByRandom(Math.PI / 4f) * 8f;

            if (projectile.timeLeft > 10)
            {
                Vector2 origin = projectile.Center - projectile.velocity * projectile.width / 2f;
                Dust.NewDustPerfect(origin, 5, Main.rand.NextFloat(0.8f, 1.7f) * dustVel, 0, default(Color), 2.5f).noGravity = true;
                Dust.QuickDust(origin, new Color(1f, 0f, 0f)).velocity = Main.rand.NextFloat(0.5f, 1.2f) * dustVel;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}