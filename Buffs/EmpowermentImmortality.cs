using Microsoft.Xna.Framework;
using Terraria;

namespace Necromancy.Buffs
{
	public class EmpowermentImmortality : Empowerment
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Empowerment - Immortality");
            Description.SetDefault("You can dig that tune... Cheat death when charged");
            Main.debuff[Type] = true;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = "Charge: " + lastPotency / 2 + "%";
        }

        public override void Tick(Player player, int potency)
        {
            player.GetModPlayer<NecromancyPlayer>().immortalEmp = potency >= 200;
            if (potency >= 200)
            {
                for (int i = 0; i < 3; i++)
                {
                    Vector2 pos = player.Center + Main.rand.NextVector2Circular(48f, 48f);
                    Dust d = Dust.NewDustPerfect(pos, 21);
                    d.noGravity = true;
                    d.velocity = player.Center - d.position;
                    d.velocity /= 8f;
                    d.velocity += player.velocity / 2f;
                }
            }
        }
    }
}
