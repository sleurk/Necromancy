using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
    public class ResurrectBone : Resurrect
    {
        protected override RitualPower Power
        {
            get { return RitualPower.RitualBone; }
        }
    }
}