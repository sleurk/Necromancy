using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Necromancy.Empowerments
{
	public class EmpowermentSummonDamage : Empowerment
    {
        public EmpowermentSummonDamage(int time, int owner, int maxTime, bool flag, int power) : base(time, owner, maxTime, flag, power) { }

        public override Color Color
        {
            get { return new Color(1f, 0f, 1f); }
        }

        public override Point Frame
        {
            get { return new Point(10, 0); }
        }

        public override string EmpDisplayName
        {
            get { return "Summon Damage"; }
        }

        public override string Text
        {
            get { return "+" + power / 4 + "%"; }
        }

        public override void Behavior()
        {
            Player.minionDamage += power / 400f;
            Player.GetModPlayer<NecromancyPlayer>().smDmgFromEmp += power / 400f;
        }
    }
}