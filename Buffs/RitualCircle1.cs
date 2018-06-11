using Terraria;
using Terraria.ModLoader;
//using Terraria.NPCs;

namespace Necromancy.Buffs
{
	public class RitualCircle1 : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Ritual Circle");
            Description.SetDefault("+10% damage, +50 max life");
			Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<NecromancyPlayer>(mod).allDamageMultiplier += 0.1f;
            player.statLifeMax2 += 50;
		}
	}
}
