using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class Cruor : ModProjectile
	{
        // creates a large projectile until the animation is over
        // only hits enemies diagonally
        // definitely a direct copy of Pulse spell from Necrodancer, mostly to see if I could
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cruor");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 236;
			projectile.height = 236;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 12;
            ProjectileID.Sets.Homing[projectile.type] = true;
            Main.projFrames[mod.ProjectileType("Cruor")] = 6;
            projectile.tileCollide = false;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).blood = true;
        }

        public override bool? CanHitNPC(NPC target)
        {
            Vector2 relative = target.Center - projectile.Center;
            if (projectile.timeLeft <= 6 || Math.Abs(Math.Abs(relative.X) - Math.Abs(relative.Y)) >= 48f) return false;
            return base.CanHitNPC(target);
        }

        public override void AI()
        {
            projectile.Center = Main.player[projectile.owner].Center;
            Lighting.AddLight(projectile.position, 0f, 0.3f, 0.5f);
            if (projectile.timeLeft % 2 == 0) projectile.frame++;
            Vector2 dustVel = new Vector2(8f).RotatedByRandom(0.2f);
            Dust.NewDustPerfect(projectile.Center, 5, Main.rand.NextFloat(0.5f, 1.2f) * dustVel.RotatedBy(Main.rand.Next(4) * MathHelper.PiOver2)).noGravity = true;
            Dust.QuickDust(projectile.Center, new Color(1f, 0f, 0f)).velocity = Main.rand.NextFloat(0.8f, 1.7f) * dustVel.RotatedBy(Main.rand.Next(4) * MathHelper.PiOver2);
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