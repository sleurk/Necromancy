using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class IchorEndurance : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Ichor Endurance");
            Description.SetDefault("Increases defense by 20");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.statDefense += 20;
        }
	}
}
