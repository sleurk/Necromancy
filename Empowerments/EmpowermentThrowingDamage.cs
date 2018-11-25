using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Necromancy.Empowerments
{
	public class EmpowermentThrowingDamage : Empowerment
    {
        public EmpowermentThrowingDamage(int time, int owner, int maxTime, bool flag, int power) : base(time, owner, maxTime, flag, power) { }

        public override Color Color
        {
            get { return new Color(1f, 0.5f, 0f); }
        }

        public override Point Frame
        {
            get { return new Point(1, 0); }
        }

        public override string EmpDisplayName
        {
            get { return "Throwing Damage"; }
        }

        public override string Text
        {
            get { return "+" + power / 4 + "%"; }
        }

        public override void Behavior()
        {
            Player.thrownDamage += power / 400f;
            Player.GetModPlayer<NecromancyPlayer>().thDmgFromEmp += power / 400f;
        }
    }
}