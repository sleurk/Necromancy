using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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

        public override void Tick()
        {
            targetPlayer = Main.player[targetItem.owner];
            if (targetItem == null || targetPlayer == null) projectile.Kill();
            targetPlayer.AddBuff(mod.BuffType<Buffs.Enchanted>(), 2);
            targetPlayer.GetModPlayer<NecromancyPlayer>().buffedWeapon.Add(targetItem.type);
            targetItem.GetGlobalItem<Items.NecromancyGlobalItem>().enchanted = Math.Max(targetItem.GetGlobalItem<Items.NecromancyGlobalItem>().enchanted, power);
        }
    }
}