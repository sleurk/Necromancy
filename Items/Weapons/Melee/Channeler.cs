using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Melee
{
	public class Channeler : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Channeler");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 63;
            item.width = 64;
			item.height = 64;
			item.useTime = 10;
			item.useAnimation = 12;
			item.useStyle = 5;
			item.knockBack = 6;
            item.channel = true;
            item.noUseGraphic = true;
			item.useTurn = false;﻿
            item.noMelee = true;
			item.rare = 7;
            item.value = Item.sellPrice(0, 5);
            item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Channeler");
			item.shootSpeed = 2.4f;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).melee = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal = 5;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            // creates spear projectile and shoots a small boomeranging projectile with it

            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), item.shoot, damage, item.knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = item;
            proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY) * 6f, mod.ProjectileType("ChannelerShot"), damage / 4, item.knockBack / 2, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "LivingHeart");
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool CanUseItem(Player player)
		{
            return player.ownedProjectileCounts[item.shoot] < 1;
		}
    }
}
