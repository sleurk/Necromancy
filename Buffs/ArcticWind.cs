using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
    public class ArcticWind : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Arctic Wind");
            Description.SetDefault("The wind will fight for you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            NecromancyPlayer modPlayer = player.GetModPlayer<NecromancyPlayer>(mod);
            if (player.ownedProjectileCounts[mod.ProjectileType("ArcticWindLeader")] > 0)
            {
                modPlayer.iceSummon = true;
            }
            if (!modPlayer.iceSummon)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}