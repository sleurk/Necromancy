using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items
{
	public class SuperGreenJuice : ModItem
	{
        private int mana = 200;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Super Green Juice");
            Tooltip.SetDefault("Endless potion");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 30;
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SuperHealingPotion);
            item.healLife = refItem.healLife;
            item.useStyle = refItem.useStyle;
            item.useTime = refItem.useTime;
            item.useAnimation = refItem.useAnimation;
            item.UseSound = refItem.UseSound;
            item.potion = true;
            item.mana = refItem.healLife;
            item.maxStack = 1;
			item.value = Item.sellPrice(0, 1, 0, 0);
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
            recipe.AddIngredient(ItemID.SuperHealingPotion, 60);
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