using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
    public class ResurrectShadow : Resurrect
    {
        protected override RitualPower Power
        {
            get { return RitualPower.RitualShadow; }
        }
    }
}