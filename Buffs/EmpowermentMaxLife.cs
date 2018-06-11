using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class EmpowermentMaxLife : Empowerment
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Empowerment - Maximum Life");
            Description.SetDefault("You can dig that tune... Max life increased");
            Main.debuff[Type] = true;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = "Maximum Life +" + lastPotency / 2;
        }

        public override void Tick(Player player, int potency)
        {
            player.statLifeMax2 += potency / 2;
        }
    }
}
