using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class CursedRage : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Cursed Rage");
            Description.SetDefault("10% increased necrotic damage");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.GetModPlayer<NecromancyPlayer>(mod).necroticMult += 0.1f;
        }
	}
}
