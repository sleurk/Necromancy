using Terraria;

namespace Necromancy.Buffs
{
	public class EmpowermentRangedDamage : Empowerment
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Empowerment - Ranged Damage");
            Description.SetDefault("You can dig that tune... Ranged damage increased");
            Main.debuff[Type] = true;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = "Ranged damage +" + lastPotency / 4 + "%";
        }

        public override void Tick(Player player, int potency)
        {
            player.rangedDamage += potency / 400f;
        }
    }
}
