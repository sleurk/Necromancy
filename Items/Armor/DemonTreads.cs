using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class DemonTreads : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demon's Treads");
            Tooltip.SetDefault("25% increased movement speed" +
                "\n40% increased life regeneration");
        }

        public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
            item.value = Item.sellPrice(0, 3);
            item.rare = 10;
			item.defense = 20;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).thoriumRarity = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.25f;
            player.GetModPlayer<NecromancyPlayer>().regenMult += 0.40f;
        }

        public override void AddRecipes()
        {
            Mod thorium = ModLoader.GetMod("ThoriumMod");
            if (thorium != null)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(thorium, "InfernoEssence");
                recipe.AddIngredient(thorium, "DeathEssence");
                recipe.AddIngredient(thorium, "OceanEssence");
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}