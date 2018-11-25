using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
    public class MeteorShowerBone : MeteorShower
    {
        protected override RitualPower Power
        {
            get { return RitualPower.RitualBone; }
        }
    }
}