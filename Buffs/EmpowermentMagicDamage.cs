using Microsoft.Xna.Framework;
using Terraria;

namespace Necromancy.Buffs
{
	public class EmpowermentMagicDamage : Empowerment
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Empowerment - Magic Damage");
            Description.SetDefault("You can dig that tune... Magic damage increased");
            Main.debuff[Type] = true;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = "Magic damage +" + lastPotency / 4 + "%";
        }

        public override void Tick(Player player, int potency)
		{
            player.magicDamage += potency / 400f;
        }
    }
}
