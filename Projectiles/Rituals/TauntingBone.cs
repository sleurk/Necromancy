using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
    public class TauntingBone : Taunting
    {
        protected override RitualPower Power
        {
            get { return RitualPower.RitualBone; }
        }
    }
}