using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class BoneplateGreaves : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boneplate Greaves");
            Tooltip.SetDefault("12% increased life regeneration" +
                "\n5% increased movement speed");
        }
        
		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
			item.value = Item.sellPrice(0, 0, 60, 0);
			item.rare = 2;
			item.defense = 3;
		}

		public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<NecromancyPlayer>().regenMult += 0.12f;
            player.moveSpeed += 0.05f;
        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bone, 25);
            recipe.AddIngredient(ItemID.DemoniteBar, 20);
            recipe.AddIngredient(mod, "BeatingHeart");
            recipe.AddTile(TileID.BoneWelder);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bone, 25);
            recipe.AddIngredient(ItemID.CrimtaneBar, 20);
            recipe.AddIngredient(mod, "BeatingHeart");
            recipe.AddTile(TileID.BoneWelder);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}