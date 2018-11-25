using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items
{
    public class LunarHamaxeWormhole : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wormhole Hamaxe");
        }

        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.LunarHamaxeNebula);
            item.damage = refItem.damage;
            item.melee = refItem.melee;
            item.width = 60;
            item.height = 50;
            item.useTime = refItem.useTime;
            item.useAnimation = refItem.useAnimation;
            item.axe = refItem.axe;
            item.hammer = refItem.hammer;
            item.useStyle = refItem.useStyle;
            item.knockBack = refItem.knockBack;
            item.value = refItem.value;
            item.rare = refItem.rare;
            item.UseSound = refItem.UseSound;
            item.autoReuse = refItem.autoReuse;
            item.tileBoost = refItem.tileBoost;
            item.value = Item.sellPrice(0, 5);
            item.GetGlobalItem<NecromancyGlobalItem>().glowMask = ModLoader.GetTexture("Necromancy/Items/LunarHamaxeWormhole_Glow");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "FragmentWormhole", 14);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}