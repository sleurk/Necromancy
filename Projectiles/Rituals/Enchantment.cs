using Necromancy.Items;
using System;
using Terraria;

namespace Necromancy.Projectiles.Rituals
{
	public abstract class Enchantment : Ritual
    {
        public Item targetItem;
        public Player targetPlayer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enchantment");
        }

        protected override int TileType
        {
            get { return mod.TileType("EnchantmentAltar"); }
        }

        public override void Tick()
        {
            if (targetItem == null)
            {
                targetItem = Main.item[(int)projectile.ai[0]];
            }
            if (targetPlayer == null && targetItem != null) targetPlayer = Main.player[targetItem.owner];
            
            if (targetItem == null) projectile.Kill();
            else targetItem.GetGlobalItem<NecromancyGlobalItem>().enchanted = Math.Max(targetItem.GetGlobalItem<NecromancyGlobalItem>().enchanted, (int)Power);
            if (targetPlayer != null)
            {
                targetPlayer.AddBuff(mod.BuffType<Buffs.Enchanted>(), 2);
                targetPlayer.GetModPlayer<NecromancyPlayer>().buffedWeapon.Add(targetItem.type);
            }
        }
    }
}