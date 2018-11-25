using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class Protected : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Protected");
            Description.SetDefault("Knockback has no effect, increased defense");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // defense buff from ritual
            player.noKnockback = true;
        }
    }
}
