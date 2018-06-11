using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class BoneplateChestpiece : ModItem
	{
        public static float necroDamage = 0.1f;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boneplate Chestpiece");
            Tooltip.SetDefault("8% increased necrotic critical strike chance");
        }
        
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 20;
            item.value = Item.sellPrice(0, 0, 60, 0);
            item.rare = 2;
			item.defense = 9;
		}

		public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<NecromancyPlayer>().necroticCritBonus += 8;
        }

		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bone, 30);
            recipe.AddIngredient(ItemID.DemoniteBar, 25);
            recipe.AddIngredient(mod, "BeatingHeart");
            recipe.AddTile(TileID.BoneWelder);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bone, 30);
            recipe.AddIngredient(ItemID.CrimtaneBar, 25);
            recipe.AddIngredient(mod, "BeatingHeart");
            recipe.AddTile(TileID.BoneWelder);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}