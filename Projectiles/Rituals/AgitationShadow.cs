using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
    public class AgitationShadow : Agitation
    {
        protected override RitualPower Power
        {
            get { return RitualPower.RitualShadow; }
        }
    }
}