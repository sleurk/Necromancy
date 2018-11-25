using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
    public class TauntingShadow : Taunting
    {
        protected override RitualPower Power
        {
            get { return RitualPower.RitualShadow; }
        }
    }
}