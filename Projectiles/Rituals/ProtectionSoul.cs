using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
	public class ProtectionSoul : Protection
    {
        protected override RitualPower Power
        {
            get { return RitualPower.RitualSoul; }
        }
    }
}