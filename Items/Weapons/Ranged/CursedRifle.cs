using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items.Weapons.Ranged
{
	public class CursedRifle : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cursed Rifle");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 75;
            item.crit = 4;
            item.width = 80;
            item.height = 30;
            item.useTime = 41;
            item.useAnimation = 41;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 8;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 4;
            item.UseSound = SoundID.Item41;
            item.shoot = mod.ProjectileType("CursedBullet");
            item.shootSpeed = 120f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).ranged = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 80;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal = 80;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CursedBar", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, -5);
        }
    }
}
