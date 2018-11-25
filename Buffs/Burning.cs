using Necromancy.NPCs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
    public class Burning : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Burning");
            Main.debuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Fire);
            npc.GetGlobalNPC<NecromancyNPC>(mod).burning = true;
        }
    }
}
