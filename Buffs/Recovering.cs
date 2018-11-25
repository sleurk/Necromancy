using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class Recovering : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Recovering");
            Description.SetDefault("Potion sickness passes faster");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }
    }
}
