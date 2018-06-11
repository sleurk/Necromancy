using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Items.Weapons.Symphonic
{
	public class BigBrass : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Big Brass");
            Tooltip.SetDefault("Creates a blast of deep bass brass doom" +
                "\nEmpowers allies with stacking damage");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 85;
            item.crit = 4;
            item.width = 34;
            item.height = 54;
            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 5;
            item.UseSound = SoundID.Item38;
            item.shoot = mod.ProjectileType("BrassPulse");
            item.shootSpeed = 3f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).symphonic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 20;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            speedX = speedX / Math.Abs(speedX) * item.shootSpeed;
            speedY = 0;
            position.Y -= 20f;
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override void UseStyle(Player player)
        {
            player.itemLocation.X -= 28f * player.direction;
            player.itemLocation.Y -= 12f;
            player.itemRotation = 0f;
        }

        public override void AddRecipes()
        {
            Mod thorium = ModLoader.GetMod("ThoriumMod");
            if (thorium != null)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(thorium, "StrangePlating", 12);
                recipe.AddIngredient(ItemID.HallowedBar, 8);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
