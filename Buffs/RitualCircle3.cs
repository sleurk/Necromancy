using Terraria;
using Terraria.ModLoader;
//using Terraria.NPCs;

namespace Necromancy.Buffs
{
	public class RitualCircle3 : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Ritual Circle");
            Description.SetDefault("+20% damage, +100 max life");
			Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<NecromancyPlayer>(mod).allDamageMultiplier += 0.20f;
            player.statLifeMax2 += 100;
		}
	}
}
