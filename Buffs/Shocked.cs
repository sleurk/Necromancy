using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
    public class Shocked : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Shocked");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            Vector2 pos = npc.position + new Vector2(Main.rand.NextFloat(npc.width), Main.rand.NextFloat(npc.height));
            Vector2 vel = (npc.Center - pos).RotatedByRandom(MathHelper.ToRadians(20)).SafeNormalize(Vector2.Zero) * 3f + npc.velocity;
            Dust d = Dust.QuickDust(pos, new Color(1f, 0.8f, 0f));
            d.velocity = vel;
            d.fadeIn = 0.1f;
            d.scale = 0.4f;

            // spreads debuff to enemies that are touching
            int type = npc.buffType[buffIndex];
            NPC[] nearby = Necromancy.NearbyNPCs(npc.Center, 32f, true, false);
            for (int i = 0; i < nearby.Length; i++) if (!nearby[i].HasBuff(type) && nearby[i].whoAmI != npc.whoAmI) nearby[i].AddBuff(type, 300);
        }
    }
}
