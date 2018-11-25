using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items
{
    public class BoneChalk : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bone Chalk");
            Tooltip.SetDefault("Use on any ritual altar");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 30;
            item.rare = 2;
            item.value = 0;
            item.consumable = true;
        }

        public override void AddRecipes()
        {
            BloodAlchemyRecipe recipe = new BloodAlchemyRecipe(mod);
            recipe.AddIngredient(ItemID.Bone, 10);
            recipe.AddTile(mod, "BloodAlchemyStation");
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }
}