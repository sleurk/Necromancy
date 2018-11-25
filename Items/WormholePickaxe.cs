using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items
{
	public class WormholePickaxe : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wormhole Pickaxe");
        }

        public override void SetDefaults()
		{
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.NebulaPickaxe);
            item.damage = refItem.damage;
			item.melee = refItem.melee;
			item.width = 36;
			item.height = 36;
			item.useTime = refItem.useTime;
			item.useAnimation = refItem.useAnimation;
			item.pick = refItem.pick;
			item.useStyle = refItem.useStyle;
			item.knockBack = refItem.knockBack;
			item.value = refItem.value;
			item.rare = refItem.rare;
			item.UseSound = refItem.UseSound;
            item.autoReuse = refItem.autoReuse;
            item.tileBoost = refItem.tileBoost;
            item.value = Item.sellPrice(0, 5);
            item.GetGlobalItem<NecromancyGlobalItem>().glowMask = ModLoader.GetTexture("Necromancy/Items/WormholePickaxe_Glow");
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "FragmentWormhole", 12);
			recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}