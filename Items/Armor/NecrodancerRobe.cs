using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class NecrodancerRobe : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Necrodancer's Robe");
            Tooltip.SetDefault("17% increased necrotic critical strike chance" +
                "\n'Reanimate!'");
        }

        public override void SetDefaults()
		{
			item.width = 34;
			item.height = 22;
			item.value = 0;
			item.rare = 6;
			item.defense = 20;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<NecromancyPlayer>().necroticCritBonus += 17;
        }

        public override void AddRecipes()
        {
            Mod thorium = ModLoader.GetMod("ThoriumMod");
            if (thorium != null)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(thorium, "CursedCloth", 18);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}