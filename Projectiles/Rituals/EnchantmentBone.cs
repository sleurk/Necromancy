using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
    public class EnchantmentBone : Enchantment
    {
        protected override RitualPower Power
        {
            get { return RitualPower.RitualBone; }
        }
    }
}