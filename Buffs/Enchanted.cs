using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class Enchanted : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Enchanted");
            Description.SetDefault("Your weapon is empowered");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }
    }
}
