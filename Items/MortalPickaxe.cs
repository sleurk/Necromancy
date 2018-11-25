using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items
{
    public class MortalPickaxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mortal Pickaxe");
            Tooltip.SetDefault("Right click to spend life for a mining boost");
        }

        public override void SetDefaults()
        {
            item.damage = 12;
            item.melee = true;
            item.width = 38;
            item.height = 38;
            item.useTime = 25;
            item.useAnimation = 25;
            item.pick = 100;
            item.useStyle = 1;
            item.knockBack = 2;
            item.value = Item.sellPrice(0, 1);
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bone, 35);
            recipe.AddIngredient(mod, "BloodEssence", 8);
            recipe.AddIngredient(mod, "BeatingHeart", 2);
            recipe.AddTile(TileID.BoneWelder);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        // right click to give buff and drain 50 health
        public override bool AltFunctionUse(Player player)
        {
            if (!Main.player[item.owner].HasBuff(mod.BuffType("MortalBoost")))
            {
                Main.PlaySound(SoundID.Item46, player.Center);
                Necromancy.DrainLife(player, 50);
                player.AddBuff(mod.BuffType("MortalBoost"), 300);
            }
            return false;
        }
    }
}