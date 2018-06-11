using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items
{
	public class BeatingHeart : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Beating Heart");
            Tooltip.SetDefault("Created by a Health Catalyst");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(10, 4));
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 28;
            item.maxStack = 999;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 12, 0);
            ItemID.Sets.AnimatesAsSoul[item.type] = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "BloodEssence", 10);
            recipe.AddIngredient(ItemID.Ruby, 2);
            recipe.AddIngredient(ItemID.HealingPotion, 15);
            recipe.AddIngredient(this);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.LifeCrystal);
            recipe.AddRecipe();
        }
    }
}