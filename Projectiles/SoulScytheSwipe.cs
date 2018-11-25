using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Projectiles
{
	public class SoulScytheSwipe : ScytheSwipe
    {
        // arkhalis-type projectile, see ScytheSwipe for actual behavior
        // this projectile specifically creates many other projectiles in a line
        // the range increases over time to a maximum of 8x the original length
        // farther away projectiles do less damage and healing
        public const int EXTENSIONS = 8;

        public override void DoSound()
        {
            if (Age == 1) base.DoSound();
        }

        private int Age
        {
            get { return (int)Math.Abs(projectile.ai[1]); }
        }

        protected override int DustType
        {
            get { return 135; }
        }

        protected override Color Color
        {
            get { return new Color(0F, 0.6F, 0.8F); }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Scythe Swipe");
            Main.projFrames[mod.ProjectileType("SoulScytheSwipe")] = 16;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.width = 90;
            projectile.height = 80;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).lifeSteal = EXTENSIONS + 1;
            projectile.ownerHitCheck = false;
        }

        protected override void OnFrameReset()
        {
            if (projectile.ai[0] > 0f && projectile.ai[1] < 0f)
            {
                if (projectile.ai[1] > -EXTENSIONS)
                { 
                    Projectile proj = Projectile.NewProjectileDirect(Main.player[projectile.owner].Center, projectile.velocity, projectile.type, 
                        (int)(projectile.damage * 0.95f), projectile.knockBack * 0.95f, projectile.owner, 0f, projectile.ai[1] - 1);
                    proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = projectile.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom;
                }
                projectile.ai[1] *= -1f;
            }
        }

        public override void PositionScythe()
        {
            base.PositionScythe();
            projectile.Center += 2f * Age * projectile.velocity;
            projectile.alpha = Age * 255 / EXTENSIONS;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).lifeSteal = EXTENSIONS + 1 - Age;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = damage - (Age * damage / EXTENSIONS);
        }
    }
}
 