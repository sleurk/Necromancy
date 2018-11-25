using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Empowerments
{
	public class EmpowermentEndurance : Empowerment
    {
        public EmpowermentEndurance(int time, int owner, int maxTime, bool flag, int power) : base(time, owner, maxTime, flag, power) { }

        public override Color Color
        {
            get { return new Color(0f, 0.5f, 1f); }
        }

        public override Point Frame
        {
            get { return new Point(7, 1); }
        }

        public override string Text
        {
            get { return "+" + power / 10 + "%"; }
        }

        public override string EmpDisplayName
        {
            get { return "Endurance"; }
        }

        public override void Behavior()
        {
            Player.endurance += power / 10;
        }
    }
}