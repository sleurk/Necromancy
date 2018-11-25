using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles.Rituals
{
    public abstract class Recovery : Ritual
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Recovery");
        }

        protected override int TileType
        {
            get { return mod.TileType("RecoveryAltar"); }
        }

        public override void Tick()
        {
            foreach (Player player in Necromancy.NearbyAllies(projectile.Center, null, 600f))
            {
                player.AddBuff(mod.BuffType<Buffs.Recovering>(), 2);
                if (player.GetModPlayer<NecromancyPlayer>().recoverTimer == 0 && player.HasBuff(BuffID.PotionSickness))
                {
                    player.buffTime[Array.IndexOf(player.buffType, BuffID.PotionSickness)]--;
                    player.GetModPlayer<NecromancyPlayer>().recoverTimer = 7 - (int)Power;
                    // level 1: 16.7% faster
                    // level 2: 20.0% faster
                    // level 3: 25.0% faster
                }
            }
        }
    }
}