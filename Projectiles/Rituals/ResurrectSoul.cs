using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
	public class ResurrectSoul : Resurrect
    {
        protected override RitualPower Power
        {
            get { return RitualPower.RitualSoul; }
        }
    }
}