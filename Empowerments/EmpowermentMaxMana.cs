using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Empowerments
{
	public class EmpowermentMaxMana : Empowerment
    {
        public EmpowermentMaxMana(int time, int owner, int maxTime, bool flag, int power) : base(time, owner, maxTime, flag, power) { }

        public override Color Color
        {
            get { return new Color(0f, 0f, 1f); }
        }

        public override Point Frame
        {
            get { return new Point(8, 0); }
        }

        public override string Text
        {
            get { return "+" + power / 2 + "%"; }
        }

        public override string EmpDisplayName
        {
            get { return "Maximum Mana"; }
        }

        public override void Behavior()
        {
            Player.statManaMax2 += (int)(Player.statManaMax * power / 200f);
        }
    }
}