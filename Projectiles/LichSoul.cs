using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Projectiles
{
	public class LichSoul : ModProjectile
	{
        private Vector2 lastMove = Vector2.Zero;
        private int baseDmg = -1;
        private float timer = 0;
        private bool channeling = true;
        private float speed = 4f;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lich's Soul");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 16;
			projectile.height = 16;
			projectile.friendly = true;
			projectile.penetrate = 2;
            projectile.extraUpdates = 3;
            projectile.alpha = 255;
			projectile.timeLeft = 1200;
            projectile.tileCollide = false;
            projectile.netImportant = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.penetrate++;
            projectile.timeLeft -= 600;
            target.AddBuff(BuffID.CursedInferno, 600);
            target.AddBuff(BuffID.OnFire, 600);
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
            projectile.ai[0] = Math.Min(projectile.timeLeft / 1200f, 3f);
            speed = projectile.ai[0] * 4f;
            timer += projectile.ai[0];
            if (baseDmg < 0)
            {
                baseDmg = projectile.damage;
            }
            projectile.scale = projectile.ai[0];
            projectile.damage = (int)(baseDmg * projectile.ai[0]);
            Player owner = Main.player[projectile.owner];
            projectile.netUpdate = true;
            if (owner.whoAmI == Main.myPlayer && channeling && owner.channel && !owner.dead)
            {
                projectile.timeLeft += 2;
                Vector2 toMouse = Main.MouseWorld - projectile.Center;
                if (toMouse.Length() > speed)
                {
                    toMouse.Normalize();
                    toMouse *= speed;
                }
                else
                {
                    projectile.timeLeft -= 4;
                }
                projectile.velocity = toMouse;
                lastMove = projectile.velocity.SafeNormalize(lastMove);
            }
            else
            {
                channeling = false;
                projectile.velocity = lastMove * speed;
            }
            Vector2 dustPos = projectile.Center + Vector2.UnitX.RotatedBy(timer / 45f) * projectile.ai[0] * projectile.width / 2;
            Dust d = Dust.NewDustDirect(dustPos + projectile.velocity, 1, 1, 6, 0, 0);
            d.noGravity = true;
            d.scale = projectile.ai[0];
            dustPos = projectile.Center + Vector2.UnitX.RotatedBy(timer / 45f + Math.PI) * projectile.ai[0] * projectile.width / 2;
            d = Dust.NewDustDirect(dustPos + projectile.velocity, 1, 1, 75, 0, 0);
            d.noGravity = true;
            d.scale = projectile.ai[0];
        }

        public override bool PreAI()
        {
            Lighting.AddLight(projectile.position, .6f, .8f, 0f);
            return true;
        }
    }
}