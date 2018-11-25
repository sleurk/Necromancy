using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Empowerments
{
	public class EmpowermentDamage : Empowerment
    {
        public EmpowermentDamage(int time, int owner, int maxTime, bool flag, int power) : base(time, owner, maxTime, flag, power) { }

        public override Color Color
        {
            get { return new Color(1f, 0f, 0f); }
        }

        public override Point Frame
        {
            get { return new Point(0, 1); }
        }

        public override string Text
        {
            get { return "+" + power / 8 + "%"; }
        }

        public override string EmpDisplayName
        {
            get { return "Damage"; }
        }

        public override void Behavior()
        {
            Player.meleeDamage += power / 800f;
            Player.rangedDamage += power / 800f;
            Player.magicDamage += power / 800f;
            Player.minionDamage += power / 800f;
            Player.thrownDamage += power / 800f;
        }
    }
}