using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Radiant
{
	public class LifeExtractor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Life Extractor");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 31;
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
            item.value = Item.sellPrice(0, 3);
            item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("LifeExtractor");
			item.shootSpeed = 5f;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).radiant = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).healPower = 30;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            Mod thorium = ModLoader.GetMod("ThoriumMod");
            if (thorium != null)
            {
                ThoriumRecipe recipe = new ThoriumRecipe(mod);
                recipe.AddIngredient(thorium, "StrangePlating", 6);
                recipe.AddIngredient(thorium, "LifeCell");
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
		}

		public override bool CanUseItem(Player player)
        {
            // can only use if there are no existing projectiles from this item
            return player.ownedProjectileCounts[item.shoot] < 1;
		}
    }
}
