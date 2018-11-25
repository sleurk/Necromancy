using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
    public class SoulHarvestBone : SoulHarvest
    {
        protected override RitualPower Power
        {
            get { return RitualPower.RitualBone; }
        }
    }
}