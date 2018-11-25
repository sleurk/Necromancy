using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace Necromancy.Projectiles
{
    // base projectile logic for an electric bolt from several lightning weapons
    // line of damage
	public abstract class ElectricBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 8;
			projectile.height = 8;
            projectile.friendly = true;
            projectile.penetrate = -1;
			projectile.timeLeft = 600;
            projectile.hide = true;
            projectile.tileCollide = false;
            projectile.extraUpdates = 600;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shock = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 4;
        }
        
        public static void PreDrawLightning(Vector2 proj1, Vector2 proj2, SpriteBatch spriteBatch, Mod mod)
        {
            int width = 10;
            int height = 22;
            int randY = Main.rand.Next(0, height);
            Vector2 targetCenter = proj2;
            Vector2 center = proj1;
            Vector2 distToProj = targetCenter - proj1;
            float projRotation = distToProj.ToRotation() - 1.57f;
            float distance = distToProj.Length();
            center -= distToProj.SafeNormalize(Vector2.Zero) * height;
            while (distance > height && !float.IsNaN(distance))
            {
                distToProj.Normalize();
                distToProj *= height;
                center += distToProj;
                distToProj = targetCenter - center;
                distance = distToProj.Length();
                Color drawColor = new Color(1f, 1f, 1f);

                // Draw chain
                spriteBatch.Draw(mod.GetTexture("Projectiles/LightningBoltEffect"), new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y),
                    new Rectangle(0, randY, width, height), drawColor, projRotation,
                    new Vector2(width / 2f, height / 2f), 1f, SpriteEffects.None, 0f);
            }
        }
    }
}