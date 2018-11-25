using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class Energized1 : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Energized");
            Description.SetDefault("+10% critical strike chance");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.GetModPlayer<NecromancyPlayer>(mod).allCritBonus += 10;
            Dust.NewDustPerfect(player.Center, 63, Main.rand.NextVector2Circular(2f, 2f));
        }
	}
}
