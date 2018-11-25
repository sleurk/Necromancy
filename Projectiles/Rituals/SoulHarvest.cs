using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Necromancy.NPCs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles.Rituals
{
    public abstract class SoulHarvest : Ritual
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SoulHarvest");
        }

        protected override int TileType
        {
            get { return mod.TileType("SoulHarvestAltar"); }
        }

        public override void Tick()
        {
            foreach (Player player in Necromancy.NearbyAllies(projectile.Center, null, 600f))
            {
                player.AddBuff(mod.BuffType<Buffs.SoulHarvest>(), 2);
            }
            foreach (NPC npc in Necromancy.NearbyNPCs(projectile.Center, 600f))
            {
                npc.GetGlobalNPC<NecromancyNPC>().soulHarvest = (int)Power;
            }
        }
    }
}