using Necromancy.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class DiscordOrb : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Orb of discord");
            Description.SetDefault("Someone is weakening you");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NecromancyNPC>().extraDmg += 0.25f;
        }
    }
}
