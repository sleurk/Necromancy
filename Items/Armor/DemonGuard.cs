using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class DemonGuard : ModItem
	{
        public static float necroDamage = 0.1f;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demon's Guard");
            Tooltip.SetDefault("35% increased necrotic critical strike chance");
        }

        public override void SetDefaults()
		{
			item.width = 34;
			item.height = 20;
            item.value = Item.sellPrice(0, 3);
            item.rare = 10;
			item.defense = 25;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).thoriumRarity = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<NecromancyPlayer>().necroticCrit += 35;
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