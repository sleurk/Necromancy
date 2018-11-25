using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
	public class TauntingSoul : Taunting
    {
        protected override RitualPower Power
        {
            get { return RitualPower.RitualSoul; }
        }
    }
}