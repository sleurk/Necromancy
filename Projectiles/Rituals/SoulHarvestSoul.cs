using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
	public class SoulHarvestSoul : SoulHarvest
    {
        protected override RitualPower Power
        {
            get { return RitualPower.RitualSoul; }
        }
    }
}