using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
    public class EnchantmentShadow : Enchantment
    {
        protected override RitualPower Power
        {
            get { return RitualPower.RitualShadow; }
        }
    }
}