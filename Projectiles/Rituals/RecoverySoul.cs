using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
	public class RecoverySoul : Recovery
    {
        protected override RitualPower Power
        {
            get { return RitualPower.RitualSoul; }
        }
    }
}