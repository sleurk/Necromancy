using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
    public class AgitationBone : Agitation
    {
        protected override RitualPower Power
        {
            get { return RitualPower.RitualBone; }
        }
    }
}