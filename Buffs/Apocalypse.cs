using Necromancy.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class Apocalypse : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Apocalypse");
            Description.SetDefault("Regenerating health");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }

        // players get regen buff
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<NecromancyPlayer>().regenModifier += 4;
        }

        // enemies get increased damage debuff
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NecromancyNPC>().extraDmg += 0.1f;
        }
    }
}
