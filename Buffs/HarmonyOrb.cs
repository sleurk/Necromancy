using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class HarmonyOrb : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Orb of harmony");
            Description.SetDefault("Someone is healing you");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

		public override void Update(Player player, ref int buffIndex)
		{
            player.GetModPlayer<NecromancyPlayer>().regenModifier += 60;
        }
	}
}
