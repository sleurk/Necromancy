using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items
{
	public class LivingHeart : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Living Heart");
            Tooltip.SetDefault("Created by a Greater Health Catalyst");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(10, 4));
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 28;
            item.maxStack = 999;
            item.rare = 7;
            item.value = Item.sellPrice(0, 4);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledHoney, 10);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 2);
            recipe.AddIngredient(ItemID.GreaterHealingPotion, 15);
            recipe.AddIngredient(this);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemID.LifeFruit);
            recipe.AddRecipe();
        }
    }
}