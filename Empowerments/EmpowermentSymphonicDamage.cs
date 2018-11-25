using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Necromancy.Empowerments
{
	public class EmpowermentSymphonicDamage : Empowerment
    {
        public EmpowermentSymphonicDamage(int time, int owner, int maxTime, bool flag, int power) : base(time, owner, maxTime, flag, power) { }

        public override Color Color
        {
            get { return new Color(0f, 1f, 0.5f); }
        }

        public override Point Frame
        {
            get { return new Point(5, 0); }
        }

        public override string EmpDisplayName
        {
            get { return "Symphonic Damage (NYI)"; }
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