using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class HealingBoost : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Healing Boost");
            Description.SetDefault("+3 life steal");
		}

		public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<NecromancyPlayer>(mod).lifeStealModifier += 3;
        }
	}
}
