using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
	public class AgitationSoul : Agitation
    {
        protected override RitualPower Power
        {
            get { return RitualPower.RitualSoul; }
        }
    }
}