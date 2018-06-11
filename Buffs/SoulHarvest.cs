using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class SoulHarvest : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Soul Harvest");
            Description.SetDefault("Killing enemies leaves their soul behind");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }
    }
}
