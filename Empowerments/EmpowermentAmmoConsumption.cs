using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Necromancy.Empowerments
{
	public class EmpowermentAmmoConsumption : Empowerment
    {
        public EmpowermentAmmoConsumption(int time, int owner, int maxTime, bool flag, int power) : base(time, owner, maxTime, flag, power) { }

        public override Color Color
        {
            get { return new Color(0f, 1f, 0f); }
        }

        public override Point Frame
        {
            get { return new Point(4, 0); }
        }

        public override string EmpDisplayName
        {
            get { return "Ammo Consumption"; }
        }

        public override string Text
        {
            get { return "+" + power / 2 + "%"; }
        }

        public override void Behavior()
        {
            Player.GetModPlayer<NecromancyPlayer>().ammoConsumeChance -= power / 200f;
        }
    }
}