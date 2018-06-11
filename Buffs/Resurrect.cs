using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class Resurrect : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Resurrect");
            Description.SetDefault("Respawn time decreased");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }
    }
}
