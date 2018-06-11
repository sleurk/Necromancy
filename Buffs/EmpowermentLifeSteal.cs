using Terraria;

namespace Necromancy.Buffs
{
	public class EmpowermentLifeSteal : Empowerment
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Empowerment - Life Steal");
            Description.SetDefault("You can dig that tune... Gain health by damaging enemies");
            Main.debuff[Type] = true;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = "Heal for " + lastPotency / 40 + "% of damage dealt";
        }

        public override void Tick(Player player, int potency)
        {
            player.GetModPlayer<NecromancyPlayer>().universalLifeSteal += potency / 4000f;
        }
    }
}
