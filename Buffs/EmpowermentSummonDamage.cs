using Terraria;

namespace Necromancy.Buffs
{
	public class EmpowermentSummonDamage : Empowerment
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Empowerment - Summon Damage");
            Description.SetDefault("You can dig that tune... Summon damage increased");
            Main.debuff[Type] = true;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = "Summon damage +" + lastPotency / 4 + "%";
        }

        public override void Tick(Player player, int potency)
        {
            player.minionDamage += potency / 400f;
        }
    }
}
