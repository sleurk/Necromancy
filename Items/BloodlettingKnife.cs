using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items
{
    public class BloodlettingKnife : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("(NYI) Bloodletting Knife");
            Tooltip.SetDefault("Drains half of your life" +
                        "\nPurifies your blood" +
                        "\n'I really should clean this...'");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.maxStack = 1;
            item.rare = 1;
            item.value = Item.sellPrice(0, 0, 25);
            item.useAnimation = 20;
            item.useTime = 20;
            item.noUseGraphic = true;
            item.useStyle = 1;
            item.UseSound = SoundID.Item46;
        }

        public override bool CanUseItem(Player player)
        {
            // can only use if the player has 50 health or more
            return player.statLife > 50;
        }

        public override bool UseItem(Player player)
        {
            // uses half of player's life to reset their impure blood (RecentHeals)
            player.GetModPlayer<NecromancyPlayer>().RecentHeals = 0;
            Necromancy.DrainLife(player, player.statLife / 2);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "BloodEssence", 5);
            recipe.AddIngredient(ItemID.SilverBar, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            // recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "BloodEssence", 5);
            recipe.AddIngredient(ItemID.TungstenBar, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            // recipe.AddRecipe();
        }
    }
}