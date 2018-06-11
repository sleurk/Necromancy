using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class Destabilized : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Destabilized");
            Description.SetDefault("Chance to teleport away from attacks");
		}

		public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<NecromancyPlayer>(mod).destabilized = true;
        }
	}
}
