using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items
{
	public class IchorBar : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ichor Bar");
        }

        public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;
			item.maxStack = 99;
			item.value = Item.sellPrice(0, 1);
			item.rare = 4;
            item.GetGlobalItem<NecromancyGlobalItem>().glowMask = ModLoader.GetTexture("Necromancy/Items/IchorBar_Glow");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrimtaneBar, 2);
            recipe.AddIngredient(ItemID.SoulofNight);
            recipe.AddIngredient(ItemID.Ichor);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 2);
            recipe.AddRecipe();
        }
    }
}