using Terraria;

namespace Necromancy.Buffs
{
	public class EmpowermentNecroticDamage : Empowerment
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Empowerment - Necrotic Damage");
            Description.SetDefault("You can dig that tune... Necrotic damage increased");
            Main.debuff[Type] = true;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = "Necrotic damage +" + lastPotency / 4 + "%";
        }

        public override void Tick(Player player, int potency)
        {
            player.GetModPlayer<NecromancyPlayer>().necroticMult += potency / 400f;
        }
    }
}
