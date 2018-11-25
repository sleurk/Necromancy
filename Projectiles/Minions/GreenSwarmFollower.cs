using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles.Minions
{
	public class GreenSwarmFollower : ModProjectile
    {
        // NYI - summons are weird
        // This is supposed to a small projectile that homes, and comes in a set
        private Vector2 targetPos;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Green Swarm");
        }

        public override void SetDefaults()
        {
            projectile.minion = true;
            projectile.width = 4;
			projectile.height = 4;
            ProjectileID.Sets.Homing[projectile.type] = true;
            projectile.friendly = true;
			projectile.timeLeft = 2;
            projectile.penetrate = -1;
            projectile.hide = true;
            projectile.ignoreWater = true;
            projectile.netImportant = true;
			projectile.tileCollide = false;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).goo = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 30;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            Projectile leader = Main.projectile[(int)Math.Round(projectile.ai[0])];
            
            if (player.GetModPlayer<NecromancyPlayer>().gooSummon && ((leader != null && leader.type == mod.ProjectileType("GreenSwarmLeader") && leader.active && leader.owner == projectile.owner) || projectile.type == mod.ProjectileType("GreenSwarmLeader")))
            {
                projectile.timeLeft = 2;
            }
            else
            {
                player.GetModPlayer<NecromancyPlayer>().gooSummon = false;
            }

            targetPos = player.Center;

            float distanceToPlayer = (player.Center - projectile.Center).Length();
            if (distanceToPlayer > 2000f) projectile.Center = player.Center;

            if (player.HasMinionAttackTargetNPC)
            {
                targetPos = Main.npc[player.MinionAttackTargetNPC].Center;
            }
            else
            {
                NPC target = GetTarget();
                if (distanceToPlayer < 1000f && target != null)
                {
                    targetPos = target.Center;
                }
            }

            Move();

            Dust.QuickDust(projectile.Center, new Color(0f, 1f, 0f)).velocity = projectile.velocity * 0.5f;
        }

        private NPC GetTarget()
        {
            return Necromancy.NearestNPC(projectile.Center, 400f, false, false);
        }

        private void Move()
        {
            float speed = projectile.velocity.Length();
            projectile.velocity = projectile.velocity * 0.9f + (targetPos - projectile.Center) * 0.1f + Main.rand.NextVector2Circular(4f, 4f);
            float newSpeed = Math.Min(projectile.velocity.Length(), 16f);
            projectile.velocity = projectile.velocity.SafeNormalize(Vector2.Zero) * speed;
        }
    }
}
