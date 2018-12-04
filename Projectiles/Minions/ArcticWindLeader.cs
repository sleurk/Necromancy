using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles.Minions
{
	public class ArcticWindLeader : ArcticWindFollower
    {
        // NYI - summons are weird
        // This summon controls movement for ArcticWindFollower projectiles
        // Acts as the front of the wind summon
        private Vector2 targetPos;
        private int timer;

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.minionSlots = 1;
            projectile.netImportant = true;
            timer = 0;
        }

        public override void AI()
        {
            timer = (timer + 1) % 30;

            if (justSummoned) JustSummoned();

            targetPos = Owner.Center; // position it will try to move towards

            float distanceToPlayerSq = (Owner.Center - projectile.Center).LengthSquared();
            if (distanceToPlayerSq > 2000f * 2000f) projectile.Center = Owner.Center;
            NPC target = GetTarget();
            if (distanceToPlayerSq < 1000f * 1000f && target != null)
            {
                targetPos = target.Center;
            }

            Move();

            if (Owner.GetModPlayer<NecromancyPlayer>().iceSummon && Owner != null && !Owner.dead)
            {
                projectile.timeLeft = 2;
            }
            else
            {
                Owner.GetModPlayer<NecromancyPlayer>().iceSummon = false;
            }

            BaseBehavior();
        }

        private NPC GetTarget()
        {
            return Necromancy.NearestNPC(projectile.Center, 400f, false, false);
        }

        private void Move()
        {
            // sine wave vertical movement
            float mult = (float)Math.Sin(MathHelper.ToRadians(timer * 12));

            projectile.position -= 16f * new Vector2(0f, mult);

            float speed = projectile.velocity.Length();

            projectile.velocity = projectile.velocity * 0.9f + (targetPos - projectile.Center) * 0.05f + Main.rand.NextVector2Circular(2f, 2f);
            float newSpeed = Math.Min(projectile.velocity.Length(), 10f);

            projectile.velocity = projectile.velocity.SafeNormalize(Vector2.Zero) * newSpeed;

            projectile.position += 16f * new Vector2(0f, mult);

            projectile.netUpdate = true;
        }
    }
}
