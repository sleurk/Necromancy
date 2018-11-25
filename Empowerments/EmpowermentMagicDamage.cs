using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Necromancy.Empowerments
{
	public class EmpowermentMagicDamage : Empowerment
    {
        public EmpowermentMagicDamage(int time, int owner, int maxTime, bool flag, int power) : base(time, owner, maxTime, flag, power) { }

        public override Color Color
        {
            get { return new Color(0f, 0.5f, 1f); }
        }

        public override Point Frame
        {
            get { return new Point(7, 0); }
        }

        public override string EmpDisplayName
        {
            get { return "Magic Damage"; }
        }

        public override string Text
        {
            get { return "+" + power / 4 + "%"; }
        }

        public override void Behavior()
        {
            Player.magicDamage += power / 400f;
            Player.GetModPlayer<NecromancyPlayer>().mgDmgFromEmp += power / 400f;
        }
    }
}