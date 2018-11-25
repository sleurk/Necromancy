using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items
{
	public class CursedBar : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cursed Bar");
        }

        public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;
			item.maxStack = 99;
            item.value = Item.sellPrice(0, 1);
            item.rare = 4;
            item.GetGlobalItem<NecromancyGlobalItem>().glowMask = ModLoader.GetTexture("Necromancy/Items/CursedBar_Glow");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DemoniteBar, 2);
            recipe.AddIngredient(ItemID.SoulofNight);
            recipe.AddIngredient(ItemID.CursedFlame);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 2);
            recipe.AddRecipe();
        }
    }
}