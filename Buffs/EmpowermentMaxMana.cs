using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class EmpowermentMaxMana : Empowerment
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Empowerment - Maximum Mana");
            Description.SetDefault("You can dig that tune... Max mana increased");
            Main.debuff[Type] = true;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = "Maximum Mana +" + lastPotency;
        }

        public override void Tick(Player player, int potency)
        {
            player.statManaMax2 += potency;
        }
    }
}
