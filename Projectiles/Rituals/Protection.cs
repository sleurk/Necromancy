using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles.Rituals
{
    public abstract class Protection : Ritual
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Protection");
        }

        public override void Tick()
        {
            foreach (Player player in Necromancy.NearbyAllies(projectile.Center, null, 600f))
            {
                player.AddBuff(mod.BuffType<Buffs.Protected>(), 2);
                player.statDefense += power * 8;
            }
        }
    }
}