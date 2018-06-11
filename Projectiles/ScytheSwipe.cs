using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace Necromancy.Projectiles
{
    public abstract class ScytheSwipe : ModProjectile
    {
        private float DistX;
        private float DistY;

        protected int dustType;
        protected float r;
        protected float g;
        protected float b;


        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Arkhalis);
            projectile.magic = true;
            projectile.melee = false;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.netImportant = true;
            projectile.penetrate = -1;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).melee = true;
        }

        public override void AI()
        {
            // code from vanilla, to change dust/light color
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.Center, true);

            projectile.soundDelay--;
            if (projectile.soundDelay <= 0)
            {
                Main.PlaySound(SoundID.Item1, projectile.Center);
                projectile.soundDelay = 16;
            }

            if (Main.myPlayer == projectile.owner)
            {
                projectile.netUpdate = true;
                if (player.channel && !player.noItems && !player.CCed)
                {
                    float scaleFactor6 = 1f;
                    if (player.inventory[player.selectedItem].shoot == projectile.type)
                    {
                        scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                    }
                    Vector2 vector20 = Main.MouseWorld - vector;
                    vector20.Normalize();
                    if (vector20.HasNaNs())
                    {
                        vector20 = Vector2.UnitX * player.direction;
                    }
                    vector20 *= scaleFactor6;
                    projectile.velocity = vector20;
                }
                else
                {
                    projectile.Kill();
                }
            }
            Vector2 vector21 = projectile.Center + projectile.velocity * 3f;
            Lighting.AddLight(vector21, r, g, b);
            
            Dust dust = Dust.NewDustDirect(vector21 - projectile.Size / 2f, projectile.width, projectile.height, dustType, projectile.velocity.X / 3f, projectile.velocity.Y / 3f, 100, default(Color), 2f);
            dust.noGravity = true;
            dust.position -= projectile.velocity;
            dust.scale = projectile.height / 300f + 0.5f;
            dust.velocity *= dust.scale;

            projectile.frameCounter++;
            if (projectile.frameCounter % 2 == 0)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame > 15)
            {
                projectile.frame = 0;
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
