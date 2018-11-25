using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class BloodclothPants : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloodcloth Pants");
            Tooltip.SetDefault("8% increased life regeneration");
        }
        
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 18;
            item.value = Item.sellPrice(0, 0, 20);
            item.rare = 1;
			item.defense = 2;
		}

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<NecromancyPlayer>().regenMult += 0.08f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "BloodEssence", 6);
            recipe.AddIngredient(ItemID.Silk, 5);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}