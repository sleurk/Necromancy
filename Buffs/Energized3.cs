using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class Energized3 : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Energized");
            Description.SetDefault("+50% critical strike chance");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.GetModPlayer<NecromancyPlayer>(mod).allCritBonus += 50;
            Dust.NewDustPerfect(player.Center, 135, Main.rand.NextVector2Circular(3f, 3f));
        }
	}
}
