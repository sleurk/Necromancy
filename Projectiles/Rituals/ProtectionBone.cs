using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
    public class ProtectionBone : Protection
    {
        protected override RitualPower Power
        {
            get { return RitualPower.RitualBone; }
        }
    }
}