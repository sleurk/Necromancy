using Necromancy.Empowerments;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
    // basic projectile, sticks to enemies on hit
	public class ElectricBall : ModProjectile
	{
        NPC stuck;
        Vector2 relative;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Electric Ball");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            // thorium.symphonic = true
            projectile.width = 48;
			projectile.height = 48;
			projectile.friendly = true;
            Main.projFrames[projectile.type] = 6;
			projectile.penetrate = 5;
            projectile.tileCollide = false;
            projectile.netImportant = true;
            projectile.timeLeft = 300;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shock = true;
            stuck = null;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 30;
        }

        public override bool? CanHitNPC(NPC target)
        {
            if ((stuck != null && stuck != target)) return false;
            return base.CanHitNPC(target);
        }

        public override bool PreAI()
        {
            if (projectile.timeLeft % 3 == 0)
            {
                projectile.frame = (projectile.frame + 1) % 6;
            }
            return true;
        }

        public override void PostAI()
        {
            Lighting.AddLight(projectile.Center, 1f, 0.8f, 0f);
        }

        public override void AI()
		{
            if (Main.myPlayer == projectile.owner)
            {
                projectile.netUpdate = true;
                projectile.rotation += Main.rand.NextFloat(0.1f);
                if (stuck != null)
                {
                    if (stuck.active && !stuck.friendly)
                    {
                        projectile.Center = stuck.Center + relative;
                    }
                    else
                    {
                        projectile.Kill();
                    }
                }
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.timeLeft = 60;
            projectile.velocity *= 0f;
            stuck = target;
            relative = projectile.Center - target.Center;
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