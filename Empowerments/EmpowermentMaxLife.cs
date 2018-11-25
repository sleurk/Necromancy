using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Empowerments
{
	public class EmpowermentMaxLife : Empowerment
    {
        public EmpowermentMaxLife(int time, int owner, int maxTime, bool flag, int power) : base(time, owner, maxTime, flag, power) { }

        public override Color Color
        {
            get { return new Color(0.5f, 1f, 0f); }
        }

        public override Point Frame
        {
            get { return new Point(3, 0); }
        }

        public override string Text
        {
            get { return "+" + power / 4 + "%"; }
        }

        public override string EmpDisplayName
        {
            get { return "Maximum Life"; }
        }

        public override void Behavior()
        {
            Player.statLifeMax2 += (int)(Player.statLifeMax * power / 400f);
        }
    }
}