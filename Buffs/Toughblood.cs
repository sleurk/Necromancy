using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class Toughblood : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Toughblood");
            Description.SetDefault("-2 life cost, +1 life steal");
		}

		public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<NecromancyPlayer>(mod).lifeCostModifier -= 2;
            player.GetModPlayer<NecromancyPlayer>(mod).lifeStealModifier += 1;
        }
	}
}
