using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
    public class Glow : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Glow");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            // doesn't really do anything except make the enemy more visible
            Dust.QuickDust(npc.Center, new Color(0.5f, 1f, 0f)).velocity = Main.rand.NextVector2Circular(8f, 8f);
        }
    }
}
