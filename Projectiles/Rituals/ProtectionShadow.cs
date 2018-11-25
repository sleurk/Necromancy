using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
    public class ProtectionShadow : Protection
    {
        protected override RitualPower Power
        {
            get { return RitualPower.RitualShadow; }
        }
    }
}