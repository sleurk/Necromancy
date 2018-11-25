using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items
{
	public class Soul3 : ModItem
    {
        // Not an inventory item - this drops from enemies while the "Soul Harvest" ritual is active
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

        // Picking up the item does not add it to the inventory, it gives the player a buff
        public override bool OnPickup(Player player)
        {
            player.AddBuff(mod.BuffType<Buffs.Energized3>(), 360);
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