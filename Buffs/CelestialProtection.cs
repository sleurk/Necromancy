using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class CelestialProtection : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Celestial Protection");
            Description.SetDefault("+30 defense");
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.statDefense += 30;
        }
	}
}
