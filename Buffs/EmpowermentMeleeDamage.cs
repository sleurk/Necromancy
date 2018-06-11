using Terraria;

namespace Necromancy.Buffs
{
	public class EmpowermentMeleeDamage : Empowerment
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Empowerment - Melee Damage");
            Description.SetDefault("You can dig that tune... Melee damage increased");
            Main.debuff[Type] = true;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = "Melee damage +" + lastPotency / 4 + "%";
        }

        public override void Tick(Player player, int potency)
        {
            player.meleeDamage += potency / 400f;
        }
    }
}
