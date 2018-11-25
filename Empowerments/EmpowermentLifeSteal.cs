using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Empowerments
{
	public class EmpowermentLifeSteal : Empowerment
    {
        public EmpowermentLifeSteal(int time, int owner, int maxTime, bool flag, int power) : base(time, owner, maxTime, flag, power) { }

        public override Color Color
        {
            get { return new Color(1f, 0f, 0.5f); }
        }

        public override Point Frame
        {
            get { return new Point(11, 1); }
        }

        public override string Text
        {
            get { return "+" + (int)(power / 10f) / 10f + "%"; }
        }

        public override string EmpDisplayName
        {
            get { return "Life Steal"; }
        }

        public override void Behavior()
        {
            Player.GetModPlayer<NecromancyPlayer>().universalLifeSteal += power / 10000f;
        }
    }
}