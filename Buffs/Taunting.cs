using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class Taunting : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Taunting");
            Description.SetDefault("Enemies are more likely to target you");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }
    }
}
