using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class AcidDisc : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acid Disc");
        }

        public override void SetDefaults()
        {
            projectile.melee = true;
            projectile.width = 64;
			projectile.height = 64;
			projectile.friendly = true;
            projectile.tileCollide = false;
			projectile.penetrate = 30;
			projectile.timeLeft = 120;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).goo = true;
        }

		public override void AI()
        {
            projectile.rotation += MathHelper.ToRadians(10);

            Vector2 vel = 32f * Vector2.UnitX.RotatedBy(MathHelper.PiOver4 + projectile.rotation);

            Dust.QuickDust(projectile.Center + vel, new Color(0f, 1f, 0f));
            Dust.QuickDust(projectile.Center - vel, new Color(0f, 1f, 0f));

            // shoots acid every 8 frames from both ends
            if (projectile.timeLeft % 8 == 0)
            {
                Vector2 pos = projectile.Center + vel;
                if (!Collision.SolidCollision(pos, 8, 8))
                {
                    Projectile proj = Projectile.NewProjectileDirect(pos, vel.RotatedBy(MathHelper.PiOver2) / 8f, mod.ProjectileType("Acid"), projectile.damage, 0f, projectile.owner);
                    proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = projectile.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom;
                }
                pos = projectile.Center - vel;
                if (!Collision.SolidCollision(pos, 8, 8))
                {
                    Projectile proj = Projectile.NewProjectileDirect(projectile.Center - vel, -vel.RotatedBy(MathHelper.PiOver2) / 8f, mod.ProjectileType("Acid"), projectile.damage, 0f, projectile.owner);
                    proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = projectile.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom;
                }
            }

            // if going back to player
            if (projectile.ai[0] == 1f)
            {
                Vector2 toPlayer = Main.player[projectile.owner].Center - projectile.Center;
                if (toPlayer.LengthSquared() < 32f * 32f)
                {
                    projectile.Kill();
                }
                toPlayer.Normalize();
                projectile.velocity = projectile.velocity * 0.95f + toPlayer;
            }
            else
            {
                if (projectile.timeLeft < 105)
                {
                    projectile.ai[0] = 1f;
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.ai[0] = 1f;
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }
            Main.PlaySound(SoundID.Item10, projectile.position);
            
            return false;
        }

        public override bool PreAI()
        {
            Lighting.AddLight(projectile.position, .6f, 0f, 0f);
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Goo"), 300);
        }
    }
}