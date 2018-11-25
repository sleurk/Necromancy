using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
	public class MeteorShowerSoul : MeteorShower
    {
        protected override RitualPower Power
        {
            get { return RitualPower.RitualSoul; }
        }
    }
}