using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class WormholeRecover : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Recovering");
            Description.SetDefault("Decreased enemy aggro, +100% life regeneration");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.aggro -= 800;
            player.GetModPlayer<NecromancyPlayer>().regenMult += 1f;
        }
    }
}
