using Necromancy.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
	public class Agitated : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Agitated");
            Description.SetDefault("Enemy spawns increased, enemies are harder to kill");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (!npc.friendly)
            {
                npc.GetGlobalNPC<NecromancyNPC>().agitated = 2;
            }
        }
    }
}
