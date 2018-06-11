using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class Energized2 : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Energized");
            Description.SetDefault("100% critical strike chance");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.GetModPlayer<NecromancyPlayer>(mod).allCritBonus += 100;
            Dust.NewDustPerfect(player.Center, 27, Main.rand.NextVector2Circular(2.5f, 2.5f));
        }
	}
}
