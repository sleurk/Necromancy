using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Necromancy.Empowerments
{
	public class EmpowermentRadiantDamage : Empowerment
    {
        public EmpowermentRadiantDamage(int time, int owner, int maxTime, bool flag, int power) : base(time, owner, maxTime, flag, power) { }

        public override Color Color
        {
            get { return new Color(1f, 1f, 0f); }
        }

        public override Point Frame
        {
            get { return new Point(2, 0); }
        }

        public override string EmpDisplayName
        {
            get { return "Radiant Damage (NYI)"; }
        }

        public override string Text
        {
            get { return "+" + power / 4 + "%"; }
        }

        public override void Behavior()
        {
            // To be implemented
        }
    }
}