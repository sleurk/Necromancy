using Terraria;
using Terraria.ModLoader;
//using Terraria.NPCs;

namespace Necromancy.Buffs
{
	public class Favored : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Favored");
            Description.SetDefault("+10% necrotic damage, -1 life cost");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<NecromancyPlayer>(mod).necroticDamage += 0.1f;
            player.GetModPlayer<NecromancyPlayer>(mod).lifeCostModifier -= 1;
        }
	}
}
