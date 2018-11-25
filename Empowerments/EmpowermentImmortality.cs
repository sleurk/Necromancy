using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Empowerments
{
	public class EmpowermentImmortality : Empowerment
    {
        public EmpowermentImmortality(int time, int owner, int maxTime, bool flag, int power) : base(time, owner, maxTime, flag, power) { }

        public override Color Color
        {
            get { return new Color(0f, 0f, 0f); }
        }

        public override Color TextColor
        {
            get { return new Color(0.4f, 0.4f, 0.4f); }
        }

        public override Point Frame
        {
            get { return new Point(12, 1); }
        }

        public override string EmpDisplayName
        {
            get { return "Immortality"; }
        }

        public override void Behavior()
        {
            if (power == 200)
            {
                Player.GetModPlayer<NecromancyPlayer>().immortalEmp = 2;
            }
        }
    }
}