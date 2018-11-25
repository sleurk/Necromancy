using Microsoft.Xna.Framework;
using Necromancy.Empowerments;
using Necromancy.NPCs;
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
            get { return true; }
        }

        public bool necrotic = false;
        public bool ichor = false;
        public bool cursedfire = false;
        public bool fire = false;
        public bool blood = false;
        public bool glow = false;
        public bool burn = false;
        public bool ice = false;
        public bool goo = false;
        public bool shock = false;
        public bool healAll = false;
        public bool melee = false;
        public bool ranged = false;
        public bool magic = false;
        public bool summon = false;
        public bool throwing = false;
        public bool radiant = false;
        public bool symphonic = false;
        public bool lightningShootFlag = false;
        public EmpType empowermentType;
        public bool rangedHit = false;
        public bool magnetActivated = false;
        public int lifeSteal = 0;
        public int healPower = 0;
        public int summonCost = 0;
        public Item shotFrom = null;

        public override void Kill(Projectile projectile, int timeLeft)
        {
            if (projectile.type != mod.ProjectileType("ABowBolt") && projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).ranged && !projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).rangedHit)
            {
                // reducing buff from Sharpshooter's Blessing after missing a projectile
                if (Main.player[projectile.owner] != null && projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom != null)
                {
                    float reduction = Main.player[projectile.owner].GetModPlayer<NecromancyPlayer>(mod).rangedHitsNum - projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom.useTime / 60f;
                    Main.player[projectile.owner].GetModPlayer<NecromancyPlayer>(mod).rangedHitsNum = Math.Max(0, reduction);
                }
            }
        }

        public override void PostAI(Projectile projectile)
        {
            // doing necrotic summon costs
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
                    player.GetModPlayer<NecromancyPlayer>().totalSummonCost += activeSummonCost;
                    player.statLifeMax2 -= activeSummonCost;
                    projectile.timeLeft = 2;
                }

                if (player.statLife > player.statLifeMax2) Necromancy.DrainLife(player, player.statLife - player.statLifeMax2, -2f);
                if (player.statLifeMax2 <= 0) player.statLifeMax2 = player.statLifeMax;
            }
        }
    }
}