using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles.Rituals
{
    public abstract class Resurrect : Ritual
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Resurrect");
        }

        public override void Tick()
        {
            foreach (Player player in Necromancy.NearbyAllies(projectile.Center, null, 600f, false, true))
            {
                player.AddBuff(mod.BuffType<Buffs.Resurrect>(), 2);
                player.GetModPlayer<NecromancyPlayer>().resurrect = power;
            }
        }
    }
}