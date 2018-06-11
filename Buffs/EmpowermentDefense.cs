using Terraria;

namespace Necromancy.Buffs
{
	public class EmpowermentDefense : Empowerment
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Empowerment - Defense");
            Description.SetDefault("You can dig that tune... Defense increased");
            Main.debuff[Type] = true;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = "Defense +" + lastPotency / 2;
        }

        public override void Tick(Player player, int potency)
        {
            player.statDefense += potency / 20;
        }
    }
}
