using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
    public class RecoveryShadow : Recovery
    {
        protected override RitualPower Power
        {
            get { return RitualPower.RitualShadow; }
        }
    }
}