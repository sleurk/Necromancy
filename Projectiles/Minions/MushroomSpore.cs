using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles.Minions
{
	public class MushroomSpore : ModProjectile
	{
        // this is a minion with movement similar to plantera/mushroom spore mini-NPCs
        // fast horizontal movement, slow vertical movement, swaying
        private Vector2 targetPos;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mushroom Spore");
        }

        public override void SetDefaults()
        {
            projectile.minion = true;
            projectile.minionSlots = 1;
            projectile.width = 12;
			projectile.height = 18;
			projectile.friendly = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            projectile.timeLeft = 2;
            projectile.penetrate = -1;
			projectile.ignoreWater = true;
            projectile.netImportant = true;
			projectile.tileCollide = false;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).glow = true;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            if (player.GetModPlayer<NecromancyPlayer>().sporeSummon && player.active && !player.dead)
            {
                projectile.timeLeft = 2;
            }
            else
            {
                player.GetModPlayer<NecromancyPlayer>().sporeSummon = false;
            }

            targetPos = player.Center;

            float distanceToPlayerSq = (player.Center - projectile.Center).LengthSquared();
            if (distanceToPlayerSq > 2000f * 2000f) projectile.Center = player.Center;
            if (player.HasMinionAttackTargetNPC)
            {
                targetPos = Main.npc[player.MinionAttackTargetNPC].Center;
            }
            else
            {
                NPC target = GetTarget();
                if (distanceToPlayerSq < 1000f * 1000f && target != null)
                {
                    targetPos = target.Center;
                }
            }
            Move();

            Lighting.AddLight(projectile.position, 0.6f, 0.9f, 0f);
        }

        private NPC GetTarget()
        {
            return Necromancy.NearestNPC(projectile.Center, 400f, false, false);
        }

        private void Move()
        {
            float moveX = (targetPos - projectile.Center).X * 0.01f;
            float moveY = Math.Sign((targetPos - projectile.Center).Y) * 0.03f;
            projectile.velocity += new Vector2(moveX, moveY);
            projectile.rotation = projectile.velocity.X * 0.05f;
            projectile.direction = Math.Sign(projectile.velocity.X);
            projectile.velocity.X = Math.Min(16f, projectile.velocity.X);
            projectile.velocity.Y = Math.Min(4f, projectile.velocity.Y);
        }
    }
}
