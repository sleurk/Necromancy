using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
    public class RecoveryBone : Recovery
    {
        protected override RitualPower Power
        {
            get { return RitualPower.RitualBone; }
        }
    }
}