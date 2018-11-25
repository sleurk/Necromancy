using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Empowerments
{
	public class EmpowermentArmorPierce : Empowerment
    {
        public EmpowermentArmorPierce(int time, int owner, int maxTime, bool flag, int power) : base(time, owner, maxTime, flag, power) { }

        public override Color Color
        {
            get { return new Color(0.5f, 1f, 0f); }
        }

        public override Point Frame
        {
            get { return new Point(3, 1); }
        }

        public override string Text
        {
            get { return "+" + power / 8; }
        }

        public override string EmpDisplayName
        {
            get { return "Armor Pierce"; }
        }

        public override void Behavior()
        {
            Player.armorPenetration += power / 8;
        }
    }
}