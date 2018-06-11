using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public abstract class Empowerment : ModBuff
	{
        protected int lastPotency = 0;

        public int EmpIndex
        {
            get
            {
                return Array.IndexOf(NecromancyPlayer.empowermentIndex, GetType());
            }
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            int[] empPotency = player.GetModPlayer<NecromancyPlayer>().empPotency;
            empPotency[EmpIndex] = Math.Min(200, empPotency[EmpIndex] + 1);
            if (player.buffType[buffIndex] == mod.BuffType<EmpowermentImmortality>() && player.GetModPlayer<NecromancyPlayer>().necrodancer)
            {
                empPotency[EmpIndex] = Math.Min(200, empPotency[EmpIndex] + 1);
            }
            return false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            lastPotency = player.GetModPlayer<NecromancyPlayer>().empPotency[EmpIndex];
            player.GetModPlayer<NecromancyPlayer>().empActiveCheck[EmpIndex] = true;

            Tick(player, player.GetModPlayer<NecromancyPlayer>().empPotency[EmpIndex]);
        }

        public abstract void Tick(Player player, int potency);
    }
}

/*
Reference:
Crit Chance				Fiddle
Damage					Big Brass
Defense					Tenor Drum
Endurance				Blues Bass
Flight					Celestial Harp
Immortality				Golden Lute
Life Steal				Seeping Song
Special Damage			Octobass
Max Life				??
Max Mana				Wight Whistle
Movement Speed			Space Drum
Regeneration			Konga's Bongos
*/