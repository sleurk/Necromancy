﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items
{
	public class Soul1 : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul");
        }

        public override void SetDefaults()
        {
            item.alpha = 150;
            item.width = 24;
			item.height = 24;
			ItemID.Sets.ItemIconPulse[item.type] = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}

        public override bool CanPickup(Player player)
        {
            return true;
        }

        public override bool OnPickup(Player player)
        {
            player.AddBuff(mod.BuffType<Buffs.Energized1>(), 120);
            Main.PlaySound(SoundID.Item9, player.Center);
            return false;
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            Player player = Necromancy.NearestPlayer(item.Center, 24f);
            if (player != null)
            {
                OnPickup(player);
                item.TurnToAir();
            }
        }
    }
}