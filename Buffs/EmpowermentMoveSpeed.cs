using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class EmpowermentMoveSpeed : Empowerment
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Empowerment - Movement Speed");
            Description.SetDefault("You can dig that tune... Movement speed increased");
            Main.debuff[Type] = true;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = "Movement Speed +" + lastPotency / 4f + "%";
        }

        public override void Tick(Player player, int potency)
        {
            player.moveSpeed += potency / 400f;
        }
    }
}
