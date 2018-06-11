using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
    public class NecromancyGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public bool necrotic = false;
        public bool ichor = false;
        public bool cursedfire = false;
        public bool fire = false;
        public bool healAll = false;
        public bool melee = false;
        public bool ranged = false;
        public bool magic = false;
        public bool summon = false;
        public bool throwing = false;
        public bool radiant = false;
        public bool symphonic = false;
        public int buffType;
        public bool rangedHit = false;
        public bool magnetActivated = false;
        public int lifeSteal = 0;
        public int healPower = 0;
        public int summonCost = 0;
        public Item shotFrom = null;

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            NecromancyPlayer mPlayer = player.GetModPlayer<NecromancyPlayer>();

            NecromancyGlobalProjectile gProjectile = projectile.GetGlobalProjectile<NecromancyGlobalProjectile>();

            if (true /*target.type != NPCID.TargetDummy && !target.SpawnedFromStatue */)
            {
                if (projectile.type == mod.ProjectileType("PestilenceBall"))
                {
                    target.GetGlobalNPC<NPCs.NecromancyNPC>(mod).pestilence = true; // no need for buff, since it lasts forever
                    target.GetGlobalNPC<NPCs.NecromancyNPC>(mod).pestilencePlayer = player; // who to heal
                }
            }
        }

        public override void Kill(Projectile projectile, int timeLeft)
        {
            if (projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).ranged && !projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).rangedHit)
            {
                Main.player[projectile.owner].GetModPlayer<NecromancyPlayer>(mod).rangedHitsNum = 0f;
            }
        }

        public override void PostAI(Projectile projectile)
        {
            if (summonCost > 0)
            {
                Player player = Main.player[projectile.owner];
                int numSummons = Necromancy.CountSummons(player);

                int activeSummonCost = summonCost;
                if (player.GetModPlayer<NecromancyPlayer>().demonCowl) activeSummonCost /= 2;
                activeSummonCost = Math.Max(activeSummonCost, 1);
                
                activeSummonCost *= numSummons;

                if (!player.dead)
                {
                    player.statLifeMax2 -= activeSummonCost;
                    projectile.timeLeft = 2;
                }

                if (player.statLife > player.statLifeMax2) Necromancy.DrainLife(player, player.statLife - player.statLifeMax2);
                if (player.statLifeMax2 <= 0) player.statLifeMax2 = player.statLifeMax;
            }
        }
    }
}