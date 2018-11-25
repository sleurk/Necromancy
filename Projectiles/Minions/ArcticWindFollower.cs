using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles.Minions
{
    public class ArcticWindFollower : ModProjectile
    {
        // NYI - summons are weird
        // This summon follows the projectile ahead of it, creating a trail that hits enemies multiple times
        protected bool justSummoned;

        private Projectile Leader
        {
            get { return Main.projectile[(int)projectile.ai[0]]; }
        }

        protected Player Owner
        {
            get { return Main.player[projectile.owner]; }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arctic Wind");
        }

        public override void SetDefaults()
        {
            ProjectileID.Sets.Homing[projectile.type] = true;
            projectile.minion = true;
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.timeLeft = 2;
            projectile.penetrate = -1;
            projectile.hide = true;
            projectile.ignoreWater = true;
            projectile.netImportant = true;
            projectile.tileCollide = false;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).ice = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 30;
            justSummoned = true;
        }

        public override void AI()
        {
            if (justSummoned) JustSummoned();

            if (Owner.HasBuff(mod.BuffType("ArcticWind")) && Leader.active && Leader.owner == projectile.owner && (Leader.type == mod.ProjectileType("ArcticWindLeader") || Leader.type == mod.ProjectileType("ArcticWindFollower")))
            {
                // follows 8 frames behind the ArcticWindLeader/ArcticWindFollower projectile it is following
                Vector2 pos = Leader.oldPos[8];
                if (pos != null && pos != Vector2.Zero)
                {
                    projectile.oldVelocity = projectile.velocity;
                    projectile.velocity = pos - projectile.position;
                }
                else
                {
                    projectile.velocity = Vector2.Zero;
                }
                projectile.timeLeft = 2;
            }

            BaseBehavior();
        }

        // things that ArcticWindFollower and ArcticWindLeader both do
        protected void BaseBehavior()
        {
            float mult = (projectile.ai[1] + 10) / 10f;
            if (Main.rand.NextFloat() > mult) Dust.QuickDust(projectile.Center, new Color(mult, mult * 0.5f + 0.5f, 1f)).velocity = projectile.velocity * mult * 0.5f + Main.rand.NextVector2Circular(1f, 1f);

            for (int i = projectile.oldPos.Length - 1; i > 0; i--)
            {
                projectile.oldPos[i] = projectile.oldPos[i - 1];
            }
            projectile.oldPos[0] = projectile.position;
        }

        protected void JustSummoned()
        {
            justSummoned = false;
            if (projectile.ai[1] + 10 > 1)
            {
                // Makes another ArcticWindFollower following this one
                Projectile proj = Projectile.NewProjectileDirect(projectile.Center, projectile.velocity, mod.ProjectileType("ArcticWindFollower"), projectile.damage, projectile.knockBack, projectile.owner,
                    projectile.whoAmI, projectile.ai[1] - 1);
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = projectile.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom;
            }
        }
    }
}
