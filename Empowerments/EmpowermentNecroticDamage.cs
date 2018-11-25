using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Necromancy.Empowerments
{
	public class EmpowermentNecroticDamage : Empowerment
    {
        public EmpowermentNecroticDamage(int time, int owner, int maxTime, bool flag, int power) : base(time, owner, maxTime, flag, power) { }

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
            get { return new Point(12, 0); }
        }

        public override string EmpDisplayName
        {
            get { return "Necrotic Damage"; }
        }

        public override string Text
        {
            get { return "+" + power / 4 + "%"; }
        }

        public override void Behavior()
        {
            Player.GetModPlayer<NecromancyPlayer>().necroticDamage += power / 400f;
        }
    }
}