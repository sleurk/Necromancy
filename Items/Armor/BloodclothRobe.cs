using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class BloodclothRobe : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloodcloth Robe");
            Tooltip.SetDefault("5% increased necrotic critical strike chance");
        }
        
		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 26;
            item.value = Item.sellPrice(0, 0, 20, 0);
            item.rare = 1;
			item.defense = 3;
		}

		public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<NecromancyPlayer>().necroticCritBonus += 5;
        }

		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "BloodEssence", 8);
            recipe.AddIngredient(ItemID.Silk, 15);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            drawHands = true;
        }
    }
}