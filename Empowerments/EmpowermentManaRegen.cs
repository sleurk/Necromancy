using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Empowerments
{
	public class EmpowermentManaRegen : Empowerment
    {
        public EmpowermentManaRegen(int time, int owner, int maxTime, bool flag, int power) : base(time, owner, maxTime, flag, power) { }

        public override Color Color
        {
            get { return new Color(0.5f, 0f, 1f); }
        }

        public override Point Frame
        {
            get { return new Point(9, 0); }
        }

        public override string Text
        {
            get { return "+" + power / 4; }
        }

        public override string EmpDisplayName
        {
            get { return "Mana Regeneration"; }
        }

        public override void Behavior()
        {
            Player.manaRegenBonus += power / 4;
        }
    }
}