using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class NecrodancerPants : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Necrodancer's Pants");
            Tooltip.SetDefault("14% increased movement speed" +
                "\n25% increased life regeneration" +
                "\n'Give me that lute!'");
        }

        public override void SetDefaults()
		{
			item.width = 30;
			item.height = 18;
			item.value = 0;
			item.rare = 6;
			item.defense = 8;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.14f;
            player.GetModPlayer<NecromancyPlayer>().regenMult += 0.25f;
        }

        public override void AddRecipes()
        {
            Mod thorium = ModLoader.GetMod("ThoriumMod");
            if (thorium != null)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(thorium, "CursedCloth", 14);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}