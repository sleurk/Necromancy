using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class DarkPower : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Dark Power");
            Description.SetDefault("+20% necrotic damage, +3 life cost");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<NecromancyPlayer>(mod).necroticDamage += 0.2f;
            player.GetModPlayer<NecromancyPlayer>(mod).lifeCostModifier += 3;
        }
	}
}
