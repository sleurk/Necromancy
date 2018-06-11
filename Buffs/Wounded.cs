using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
    public class Wounded : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Wounded");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCs.NecromancyNPC>(mod).wounded = true;
        }
    }
}
