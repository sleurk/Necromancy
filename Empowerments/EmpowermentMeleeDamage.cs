using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Empowerments
{
	public class EmpowermentMeleeDamage : Empowerment
    {
        public EmpowermentMeleeDamage(int time, int owner, int maxTime, bool flag, int power) : base(time, owner, maxTime, flag, power) { }

        public override Color Color
        {
            get { return new Color(1f, 0f, 0f); }
        }

        public override Point Frame
        {
            get { return new Point(0, 0); }
        }

        public override string Text
        {
            get { return "+" + power / 4 + "%"; }
        }

        public override string EmpDisplayName
        {
            get { return "Melee Damage"; }
        }

        public override void Behavior()
        {
            Player.meleeDamage += power / 400f;
            Player.GetModPlayer<NecromancyPlayer>().mlDmgFromEmp += power / 400f;
        }
    }
}