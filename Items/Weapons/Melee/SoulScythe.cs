using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Melee
{
    // this is a child of SwipeWeapon.cs, so the important code is there

    public class SoulScythe : SwipeWeapon
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Scythe");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 67;
            item.width = 74;
			item.height = 68;
            item.knockBack = 4f;
            item.value = Item.sellPrice(0, 10);
            item.rare = 8;
            item.shoot = mod.ProjectileType("SoulScytheSwipe");
            item.shootSpeed = 32f;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal = SoulScytheSwipe.EXTENSIONS + 1;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), item.shoot, damage, knockBack, player.whoAmI, 0f, -1f);
            // ai1 = projectile's "age" (see SoulScytheSwipe.cs)
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Ectoplasm, 6);
            recipe.AddIngredient(ItemID.SpookyWood, 30);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}