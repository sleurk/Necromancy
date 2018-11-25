using Microsoft.Xna.Framework;
using Necromancy.NPCs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
    public class Goo : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Goo");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.t_Slime, 0f, 0f, 0, new Color(0.5f, 1f, 0.6f));
            d.velocity *= 0.4f;
            d.noGravity = true;
            npc.GetGlobalNPC<NecromancyNPC>().goo = true;
        }
    }
}
