using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class VampiricExhaustion : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Vampiric Exhaustion");
            Description.SetDefault("Your vampire locket is recharging");
            Main.debuff[Type] = true;
        }
    }
}
