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
        }

        public override void SetDefaults()
        {
            item.damage = 12;
            item.melee = true;
            item.width = 38;
            item.height = 38;
            item.useTime = 15;
            item.useAnimation = 15;
            item.pick = 100;
            item.useStyle = 1;
            item.knockBack = 2;
            item.value = Item.sellPrice(0, 0, 54, 0);
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 2;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeCost = 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bone, 35);
            recipe.AddIngredient(null, "BloodEssence", 8);
            recipe.AddIngredient(null, "BeatingHeart", 8);
            recipe.AddTile(TileID.BoneWelder);
            recipe.SetResult(this);
            // recipe.AddRecipe();
        }

        public override bool UseItem(Player player)
        {
            player.statLife -= item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeCost;
            if (player.statLife <= 0)
            {
                Terraria.DataStructures.PlayerDeathReason damageSource = Terraria.DataStructures.PlayerDeathReason.ByCustomReason(" ran out of blood.");
                player.KillMe(damageSource, 5, -player.direction);
            }
            return true;
        }
    }
}