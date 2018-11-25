using Necromancy.NPCs;
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
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NecromancyNPC>(mod).wounded = true;
        }
    }
}
