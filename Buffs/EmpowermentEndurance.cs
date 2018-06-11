using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class EmpowermentEndurance : Empowerment
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Empowerment - Endurance");
            Description.SetDefault("You can dig that tune... Endurance increased");
            Main.debuff[Type] = true;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = "Damage taken -" + lastPotency / 10 + "%";
        }

        public override void Tick(Player player, int potency)
        {
            player.endurance += potency / 10;
        }
    }
}
