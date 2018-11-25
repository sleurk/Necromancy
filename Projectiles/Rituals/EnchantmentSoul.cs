using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
	public class EnchantmentSoul : Enchantment
    {
        protected override RitualPower Power
        {
            get { return RitualPower.RitualSoul; }
        }
    }
}