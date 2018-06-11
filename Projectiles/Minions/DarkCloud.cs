using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Projectiles.Minions
{
    public class DarkCloud : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.width = 16;
            projectile.height = 16;
            Main.projFrames[projectile.type] = 6;
            projectile.penetrate = -1;
            projectile.timeLeft = 3600;
            projectile.hide = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).summon = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).summonCost = 50;
        }

        public override void AI()
        {
            CheckActive();
            Behavior();
        }

        public void SelectFrame()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 6;
            }
        }

        public void CreateDust()
        {
            Dust.NewDustPerfect(projectile.Center, 54, projectile.velocity).noGravity = true;
        }

        public void CheckActive()
        {
            Player player = Main.player[projectile.owner];
            if (!player.dead)
            {
                projectile.timeLeft = 2;
            }
        }

        public void Behavior()
        {
            NPC npc = Necromancy.NearestNPC(projectile.Center, 200f);
            Vector2 target = Main.player[projectile.owner].Center;
            if (npc != null && npc.active && !npc.friendly)
            {
                target = npc.Center;
            }

            Vector2 toTarget = target - projectile.Center;
            projectile.velocity = projectile.velocity * 0.95f + toTarget * 0.05f + Main.rand.NextVector2Circular(2f, 2f);

            CreateDust();
        }
    }
}
