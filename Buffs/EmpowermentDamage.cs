using Terraria;

namespace Necromancy.Buffs
{
	public class EmpowermentDamage : Empowerment
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Empowerment - Damage");
            Description.SetDefault("You can dig that tune... Damage increased");
            Main.debuff[Type] = true;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = "Damage +" + lastPotency / 8 + "%";
        }

        public override void Tick(Player player, int potency)
        {
            player.meleeDamage += potency / 800f;
            player.rangedDamage += potency / 800f;
            player.magicDamage += potency / 800f;
            player.minionDamage += potency / 800f;
            player.thrownDamage += potency / 800f;
            player.GetModPlayer<NecromancyPlayer>().dmgEmp = potency / 800f;
        }
    }
}
