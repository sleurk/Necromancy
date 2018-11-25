using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class MortalBoost : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Mortal Boost");
            Description.SetDefault("Increased mining speed");
            Main.buffNoSave[Type] = true;
        }

		public override void Update(Player player, ref int buffIndex)
		{
            if (player.HeldItem.type == mod.ItemType("MortalPickaxe")) player.pickSpeed /= 2f;
        }
	}
}
