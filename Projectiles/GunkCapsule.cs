using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Achievements;
using Microsoft.Xna.Framework.Graphics;

namespace Necromancy.Projectiles
{
    public class GunkCapsule : ModProjectile
    {
        // thrown projectile, explodes into several Gunk projectiles on contact

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gunk Capsule");
        }

        public override void SetDefaults()
        {
            projectile.thrown = true;
            projectile.width = 34;
            projectile.height = 34;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 120;
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

        public override void AI()
        {
            projectile.rotation += projectile.velocity.X / 30f;
            if (projectile.ai[0] == 0f) projectile.velocity.Y += 0.3f;
            if (projectile.ai[0] == 1f && projectile.ai[1] == 5f)
            {
                for (int i = 0; i < 12; i++)
                {
                    Dust.QuickDust(projectile.Center + Main.rand.NextVector2Square(-52f, 52f), Color.Purple);
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            if (projectile.ai[0] == 0f) Explode();
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[0] == 0f) Explode();
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X / 1.2f;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y / 3f;
            }
            projectile.velocity.X *= 0.99f;
            return false;
        }

        private void Explode()
        {
            Main.PlaySound(SoundID.Item27, projectile.Center);
            int numProjectiles = Main.rand.Next(3, 6);
            for (int i = 0; i < numProjectiles; i++)
            {
                Projectile proj = Projectile.NewProjectileDirect(projectile.Center, projectile.velocity, mod.ProjectileType("Gunk"), projectile.damage, projectile.knockBack, projectile.owner);
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = projectile.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom;
                proj.velocity = proj.velocity.SafeNormalize(Vector2.Zero) * 6f + Main.rand.NextVector2Circular(2f, 2f);
            }
        }
    }
}