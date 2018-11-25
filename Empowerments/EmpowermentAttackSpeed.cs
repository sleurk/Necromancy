using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Empowerments
{
	public class EmpowermentAttackSpeed : Empowerment
    {
        public EmpowermentAttackSpeed(int time, int owner, int maxTime, bool flag, int power) : base(time, owner, maxTime, flag, power) { }

        public override Color Color
        {
            get { return new Color(0f, 1f, 1f); }
        }

        public override Point Frame
        {
            get { return new Point(6, 1); }
        }

        public override string Text
        {
            get { return "+" + power / 8 + "%"; }
        }

        public override string EmpDisplayName
        {
            get { return "Attack Speed"; }
        }

        public override void Behavior()
        {
            Player.GetModPlayer<NecromancyPlayer>().attackSpeed += power / 800f;
        }
    }
}