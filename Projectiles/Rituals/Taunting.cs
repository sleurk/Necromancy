using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles.Rituals
{
	public abstract class Taunting : Ritual
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Taunting");
        }

        protected override int TileType
        {
            get { return mod.TileType("TauntingAltar"); }
        }

        public override void Tick()
        {
            Player targetPlayer = Main.player[projectile.owner];
            targetPlayer.GetModPlayer<NecromancyPlayer>().taunt = (int)Power + 1;
            targetPlayer.AddBuff(mod.BuffType("Taunting"), 2);
        }
    }
}