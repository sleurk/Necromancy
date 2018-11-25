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
        // arkhalis-type projectile
        // stays on player, turns to match mouse
        // loops through animation and stays alive as long as player is clicking
        protected virtual int DustType
        {
            get { return 0; }
        }
        protected virtual Color Color
        {
            get { return new Color(0F, 0F, 0F); }
        }

        protected virtual int FrameLength
        {
            get { return 2; }
        }

        protected int HitTime
        {
            get { return FrameLength * 4; }
        }

        protected Player Owner
        {
            get { return Main.player[projectile.owner]; }
        }

        protected int Timer
        {
            get { return (int)projectile.ai[0]; }
            set { projectile.ai[0] = value; }
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.melee = false;
            projectile.friendly = true;
            projectile.timeLeft = 10;
            projectile.tileCollide = false;
            projectile.netImportant = true;
            projectile.ownerHitCheck = true;
            projectile.penetrate = -1;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).melee = true;
        }

        public virtual void SelectFrame()
        {
            projectile.frame = (Timer / FrameLength) % 16;
        }

        protected virtual void OnFrameReset() { }

        public virtual void DoSound()
        {
            if (projectile.soundDelay <= 0)
            {
                Main.PlaySound(SoundID.Item1, projectile.Center);
                projectile.soundDelay = HitTime;
            }
        }

        public virtual void PositionScythe()
        {
            projectile.Center = Owner.Center;
            if (Main.myPlayer == projectile.owner)
            {
                projectile.netUpdate = true;
                if (Main.LocalPlayer.channel && !Main.LocalPlayer.noItems && !Main.LocalPlayer.CCed)
                {
                    projectile.timeLeft = 10;
                }
                else
                {
                    projectile.Kill();
                }
                float distance = 1f;
                if (Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem].shoot == projectile.type)
                {
                    distance = Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem].shootSpeed * projectile.scale;
                }
                Vector2 toMouse = Main.MouseWorld - Main.LocalPlayer.Center;
                toMouse.Normalize();
                if (toMouse.HasNaNs())
                {
                    toMouse = Vector2.UnitX * Main.LocalPlayer.direction;
                }
                projectile.velocity = toMouse * distance;
                if (projectile.velocity.X != 0) Owner.direction = Math.Sign(projectile.velocity.X);
            }
        }

        public virtual void Visuals()
        {
            Lighting.AddLight(projectile.Center, Color.ToVector3());
            Vector2 pos = (projectile.Center + projectile.velocity - Owner.Center).Length() * Vector2.UnitX;
            Vector2 box = new Vector2(Main.rand.NextFloat(-projectile.height / 2f, projectile.height / 2f), Main.rand.NextFloat(-projectile.width / 2f, projectile.width / 2f));
            pos += box;
            pos = pos.RotatedBy(projectile.velocity.ToRotation()) + Owner.Center;
            Dust dust = Dust.NewDustPerfect(pos, DustType, projectile.velocity / 3f, 100, default(Color), 2f);
            dust.noGravity = true;
            dust.scale = projectile.height / 300f + 0.5f;
            dust.velocity *= dust.scale;

            projectile.rotation = projectile.velocity.RotatedBy(MathHelper.PiOver2).ToRotation();
        }

        public override void AI()
        {
            SelectFrame();
            if (Timer % (16 * FrameLength) == 0)
            {
                OnFrameReset();
            }
            DoSound();
            PositionScythe();
            Visuals();
            Timer++;
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

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = HitTime;
        }
    }
}
