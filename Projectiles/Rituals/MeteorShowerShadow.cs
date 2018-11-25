using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
    public class MeteorShowerShadow : MeteorShower
    {
        protected override RitualPower Power
        {
            get { return RitualPower.RitualShadow; }
        }
    }
}