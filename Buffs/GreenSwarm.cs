using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
    public class GreenSwarm : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Green Swarm");
            Description.SetDefault("The swarm will fight for you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            NecromancyPlayer modPlayer = player.GetModPlayer<NecromancyPlayer>(mod);
            if (player.ownedProjectileCounts[mod.ProjectileType("GreenSwarmLeader")] > 0)
            {
                modPlayer.gooSummon = true;
            }
            if (!modPlayer.gooSummon)
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