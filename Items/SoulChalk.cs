using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;

namespace Necromancy.Items
{
    public class SoulChalk : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Chalk");
            Tooltip.SetDefault("Use on any ritual altar");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 30;
            item.rare = 8;
            item.value = Item.sellPrice(0, 0, 0, 50);
            item.consumable = true;
        }

        public override void AddRecipes()
        {
            BloodAlchemyRecipe recipe = new BloodAlchemyRecipe(mod);
            recipe.AddIngredient(ItemID.Ectoplasm, 10);
            recipe.AddTile(mod, "BloodAlchemyStation");
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }
}