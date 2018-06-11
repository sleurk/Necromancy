using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class EmpowermentRegen : Empowerment
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Empowerment - Regeneration");
            Description.SetDefault("You can dig that tune... Health regeneration increased");
            Main.debuff[Type] = true;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = "Health Regeneration +" + lastPotency / 10;
        }

        public override void Tick(Player player, int potency)
        {
            player.GetModPlayer<NecromancyPlayer>().regenModifier += potency / 10;
        }
    }
}
