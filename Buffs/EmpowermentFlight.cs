using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class EmpowermentFlight : Empowerment
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Empowerment - Flight");
            Description.SetDefault("You can dig that tune... Flight time increased");
            Main.debuff[Type] = true;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = "Flight time +" + lastPotency / 2f + "%";
        }

        public override void Tick(Player player, int potency)
        {
            if (player.wingTime > 0)
            {
                player.wingTime += potency / 400f;
            }
        }
    }
}
