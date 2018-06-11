﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items
{
	public class LesserGreenJuice : ModItem
	{
        private int mana = 50;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lesser Green Juice");
            Tooltip.SetDefault("Endless potion");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 26;
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.LesserHealingPotion);
            item.healLife = refItem.healLife;
            item.useStyle = refItem.useStyle;
            item.useTime = refItem.useTime;
            item.useAnimation = refItem.useAnimation;
            item.UseSound = refItem.UseSound;
            item.potion = true;
            item.maxStack = 1;
			item.value = Item.sellPrice(0, 0, 25, 0);
            item.rare = refItem.rare;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.potionDelay == 0 && player.statMana > mana)
            {
                player.statMana -= mana;
                return true;
            }
            return false;
        }

        public override bool UseItem(Player player)
        {
            player.potionDelay = player.potionDelayTime;
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LesserHealingPotion, 60);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool ConsumeItem(Player player)
        {
            return false;
        }
    }
}