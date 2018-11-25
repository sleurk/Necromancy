using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
    public class Chilled : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Chilled");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        // caps enemies' speed if they aren't a boss
        public override void Update(NPC npc, ref int buffIndex)
        {
            float speed = Math.Min(npc.velocity.Length(), 4f);
            if (!npc.boss) npc.velocity = npc.velocity.SafeNormalize(Vector2.Zero) * speed;
            Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.t_Frozen).velocity = Vector2.Zero;
        }
    }
}
