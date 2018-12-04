using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class LightningShock : ModProjectile
	{
        // lightning-esque projectile
        // splits randomly after a certain distance
        // bounces
        public int Length
        {
            get { return (int)projectile.ai[0]; }
            set { projectile.ai[0] = value; }
        }
        public int TimeUntilTurn
        {
            get { return (int)projectile.ai[1]; }
            set { projectile.ai[1] = value; }
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning Shock");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 4;
			projectile.height = 4;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 600;
            projectile.extraUpdates = 100;
            projectile.netImportant = true;
            projectile.hide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shock = true;
            Length = 0;
            TimeUntilTurn = 0;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }

            return false;
        }

        public float Factor
        {
            get { return (1f - 1f / Length); }
        }

        public override void AI()
        {
            if (projectile.timeLeft % 6 == 0) Dust.QuickDust(projectile.Center, new Color(1f, 1f, 0.5f));
            if (projectile.velocity.LengthSquared() > 1f)
            {
                Length = (int)projectile.velocity.Length();
                TimeUntilTurn = Length;
                projectile.velocity = projectile.velocity.RotatedByRandom(MathHelper.Pi * Factor / 8f);
                projectile.velocity.Normalize();
            }
            if (TimeUntilTurn == 0)
            {
                projectile.velocity = projectile.velocity.RotatedByRandom(MathHelper.Pi * Factor / 8f);
                TimeUntilTurn = Length;
                projectile.netUpdate = true;
                if (Main.rand.NextFloat() < Math.Pow(Factor, 24))
                {
                    Projectile proj = Projectile.NewProjectileDirect(projectile.Center, projectile.velocity.RotatedByRandom(MathHelper.Pi * Factor / 8f) * Length, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, Main.rand.NextBool() ? 1f : 0f);
                    proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom;
                    proj.timeLeft = (int)(projectile.timeLeft * Main.rand.NextFloat());
                    proj.netUpdate = true;
                }
            }
            TimeUntilTurn--;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 2;
        }
    }
}