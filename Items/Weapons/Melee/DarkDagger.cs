using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Melee
{
    // this is a child of SwipeWeapon.cs, so the important code is there

    public class DarkDagger : SwipeWeapon
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Dagger");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 15;
            item.width = 32;
            item.height = 32;
            item.knockBack = 1f;
            item.value = Item.sellPrice(0, 0, 80);
            item.rare = 2;
            item.shoot = mod.ProjectileType("DarkDaggerSwipe");
            item.shootSpeed = 64f;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal = 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DemoniteBar, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}