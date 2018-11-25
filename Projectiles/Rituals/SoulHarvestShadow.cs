using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
    public class SoulHarvestShadow : SoulHarvest
    {
        protected override RitualPower Power
        {
            get { return RitualPower.RitualShadow; }
        }
    }
}