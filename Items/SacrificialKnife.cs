using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items
{
    public class SacrificialKnife : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sacrificial Knife");
            Tooltip.SetDefault("Uses 100 life" +
                        "\nIncreases your healing power for 10 seconds" +
                        "\n'I really should clean this...'");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.maxStack = 1;
            item.rare = 1;
            item.value = Item.sellPrice(0, 0, 25, 0);
            item.useAnimation = 20;
            item.useTime = 20;
            item.noUseGraphic = true;
            item.useStyle = 1;
            item.UseSound = SoundID.Item46;
        }

        public override bool UseItem(Player player)
        {
            player.statLife -= 100;
            if (player.statLife <= 0)
            {
                Terraria.DataStructures.PlayerDeathReason damageSource = Terraria.DataStructures.PlayerDeathReason.ByCustomReason(" ran out of blood.");
                player.KillMe(damageSource, 5, -player.direction);
            }
            player.AddBuff(mod.BuffType<Buffs.HealingBoost>(), 600); // 10 secs
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "BloodEssence", 5);
            recipe.AddIngredient(ItemID.SilverBar, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "BloodEssence", 5);
            recipe.AddIngredient(ItemID.TungstenBar, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}