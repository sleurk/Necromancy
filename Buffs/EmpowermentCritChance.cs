using Terraria;

namespace Necromancy.Buffs
{
	public class EmpowermentCritChance : Empowerment
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Empowerment - Critical Chance");
            Description.SetDefault("You can dig that tune... Critical strike chance increased");
            Main.debuff[Type] = true;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = "Critical chance +" + lastPotency / 8 + "%";
        }

        public override void Tick(Player player, int potency)
        {
            player.GetModPlayer<NecromancyPlayer>().allCritBonus += potency / 8;
        }
    }
}
