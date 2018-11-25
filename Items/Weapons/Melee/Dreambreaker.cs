using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Melee
{
	public class Dreambreaker : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dreambreaker");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 37;
            item.width = 34;
			item.height = 32;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = 5;
			item.knockBack = 6;
            item.channel = true;
            item.noUseGraphic = true;
			item.useTurn = false;﻿
            item.noMelee = true;
			item.rare = 5;
            item.value = Item.sellPrice(0, 2);
            item.UseSound = SoundID.Item1;
			item.shoot = mod.ProjectileType("Dreambreaker");
			item.shootSpeed = 2.4f;
            item.GetGlobalItem<NecromancyGlobalItem>().necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>().melee = true;
            item.GetGlobalItem<NecromancyGlobalItem>().lifeSteal = 5;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            // can only use if there are no existing projectiles from this item
            return player.ownedProjectileCounts[item.shoot] == 0;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
