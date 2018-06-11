using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class WormholeDefense : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Defending");
            Description.SetDefault("Increased enemy aggro, +20 defense");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 20;
            player.aggro += 800;
        }
    }
}
