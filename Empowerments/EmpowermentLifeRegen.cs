using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Empowerments
{
	public class EmpowermentLifeRegen : Empowerment
    {
        public EmpowermentLifeRegen(int time, int owner, int maxTime, bool flag, int power) : base(time, owner, maxTime, flag, power) { }

        public override Color Color
        {
            get { return new Color(1f, 0f, 0.5f); }
        }

        public override Point Frame
        {
            get { return new Point(11, 0); }
        }

        public override string Text
        {
            get { return "+" + power / 10; }
        }

        public override string EmpDisplayName
        {
            get { return "Life Regeneration"; }
        }

        public override void Behavior()
        {
            Player.GetModPlayer<NecromancyPlayer>().regenModifier += power / 10;
        }
    }
}