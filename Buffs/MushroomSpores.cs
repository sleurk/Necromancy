using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Buffs
{
    public class MushroomSpores : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Mushroom Spores");
            Description.SetDefault("The spores will fight for you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            NecromancyPlayer modPlayer = player.GetModPlayer<NecromancyPlayer>(mod);
            if (player.ownedProjectileCounts[mod.ProjectileType("MushroomSpore")] > 0)
            {
                modPlayer.sporeSummon = true;
            }
            if (!modPlayer.sporeSummon)
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