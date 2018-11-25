using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles.Minions
{
	public class Leech : ModProjectile
	{
        // this is supposed to crawl on the ground and jump at enemies
        // but it also just breaks a lot of the time and can climb walls with its mouth
        // so this is another NYI
        private NPC target = null;
        private NPC attached = null;
        private Vector2 attachRelative = Vector2.Zero;
        private bool flying = false;
        private bool onGround = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Leech");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            Main.projFrames[projectile.type] = 6;
            ProjectileID.Sets.Homing[projectile.type] = true;
            projectile.width = 48;
			projectile.height = 48;
			projectile.timeLeft = 18000;
            projectile.penetrate = -1;
			projectile.ignoreWater = true;
            projectile.netImportant = true;
			projectile.tileCollide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).summon = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).summonCost = 10;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (oldVelocity.Y != projectile.velocity.Y)
            {
                projectile.velocity.Y = 0f;
                onGround = true;
            }
            return false;
        }

        public override void AI()
        {
            Dust d = Dust.NewDustDirect(projectile.position + new Vector2(0, 36f), projectile.width, projectile.height - 36, 18);
            d.noGravity = true;
            d.velocity = Main.rand.NextVector2Circular(3f, 3f);

            if (target != null)
            {
                if (Collision.CanHitLine(projectile.Center, 1, 1, target.Center, 1, 1) || !target.active)
                {
                    target = null;
                }
            }

            projectile.friendly = target != null;

            Player player = Main.player[projectile.owner];
            if (!player.dead)
            {
                projectile.timeLeft = 2;
            }

            float distanceToPlayer = (projectile.Center - player.Center).Length();

            // start flying
            if (distanceToPlayer > 1000f && !flying)
            {
                if (distanceToPlayer > 3000f)
                {
                    projectile.Center = player.Center;
                }
                else
                {
                    projectile.velocity = Vector2.Zero;
                    flying = true;
                }
            }

            // flying skips ai
            if (flying)
            {
                projectile.direction = Math.Sign(projectile.velocity.X);
                projectile.spriteDirection = Math.Sign(projectile.velocity.X);
                attached = null;
                projectile.frame = 4;
                projectile.tileCollide = false;
                projectile.velocity = projectile.velocity * 0.995f + ((player.Center + new Vector2(0, -16f)) - projectile.Center) * 0.005f;
                if (projectile.velocity.Length() > 20f) projectile.velocity = projectile.velocity.SafeNormalize(Vector2.Zero) * 20f;
                // stop flying
                if (distanceToPlayer < 50f && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                {
                    projectile.tileCollide = true;
                    flying = false;
                    projectile.velocity.X *= 0.2f;
                }
                return;
            }
            else
            {
                projectile.tileCollide = true;
            }

            // attaching skips ai
            if (attached != null && attached.active)
            {
                projectile.Center = attached.Center + attachRelative;
                projectile.frame = 5;
                return;
            }
            else
            {
                attachRelative = Vector2.Zero;
            }

            projectile.direction = Math.Sign(projectile.velocity.X);
            projectile.spriteDirection = Math.Sign(projectile.velocity.X);

            // gravity
            projectile.velocity.Y += 0.5f;

            attached = null;

            // find new target
            if (target == null)
            {
                NPC[] targets = Necromancy.NearbyNPCs(projectile.Center, 600f, true);
                foreach (NPC t in targets)
                {
                    if (target == null || (target.Center - projectile.Center).Length() > (t.Center - projectile.Center).Length() && target.CanBeChasedBy(this, false) && !target.friendly
                     && !Collision.CanHitLine(projectile.Center, 1, 1, t.Center, 1, 1))
                    {
                        target = t;
                    }
                }
            }

            // chasing target
            if (target != null && target.active)
            {
                projectile.velocity.X = projectile.velocity.X * 0.95f + 0.001f * (target.Center.X - projectile.Center.X);
                float dx = target.Center.X - projectile.Center.X;
                float dy = target.Center.Y - projectile.Center.Y;

                // jump
                if (onGround && Math.Abs(dx) < 50f && dy < -10f && dy > -400f)
                {
                    projectile.velocity.Y = -16f;
                }
            }
            else if (distanceToPlayer > 50f)
            {
                if (projectile.velocity.X == 0f) projectile.velocity.Y = -5f;
                projectile.velocity.X = projectile.velocity.X * 0.95f + 0.001f * (player.Center.X - projectile.Center.X);
            }
            else
            {
                projectile.velocity.X += Main.rand.NextFloat(-2f, 2f);
                projectile.velocity.X *= 0.99f;
            }

            if (Math.Abs(projectile.velocity.Y) > 16f)
            {
                projectile.frame = 4;
                return;
            }

            if (projectile.frame > 3)
            {
                projectile.frame = 0;
            }

            if (projectile.velocity.X != 0)
            {
                projectile.frameCounter++;
            }
            if (projectile.frameCounter > 10)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
        }

        public override void PostAI()
        {
            Collision.StepUp(ref projectile.position, ref projectile.velocity, projectile.width, projectile.height, ref projectile.stepSpeed, ref projectile.gfxOffY, 1, false, 0);

            if (projectile.velocity.Length() > 32f)
            {
                projectile.velocity = projectile.velocity.SafeNormalize(Vector2.Zero) * 32f;
            }
            onGround = false;
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (target.type == NPCID.TargetDummy) return false;
            return base.CanHitNPC(target);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (attached == null)
            {
                attached = target;
                attachRelative = Main.rand.NextVector2Circular(6f, 6f);
                attachRelative *= 0.8f;
            }
            Necromancy.BroadcastHealPlayer(Main.player[projectile.owner], damage / 10);
        }
    }
}
