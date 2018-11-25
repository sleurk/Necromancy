using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Magic
{
	public class Hellfire : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellfire");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 41;
            item.width = 28;
			item.height = 30;
			item.useTime = 20;
			item.useAnimation = 20;
            item.useStyle = 5;
            item.knockBack = 2;
			item.value = Item.sellPrice(0, 1);
			item.rare = 3;
            item.noMelee = true;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("Flame");
            item.shootSpeed = 20f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeCost = 7;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float numberProjectiles = 9;
            float rotation = MathHelper.ToRadians(5);
            for (int i = 0; i < numberProjectiles; i++) // 9 projectiles separated by 5 degrees each to create an arc of fire
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(rotation * (i - numberProjectiles / 2)); // to center arc on cursor aim direction
                Projectile proj = Projectile.NewProjectileDirect(position, perturbedSpeed, type, damage, knockBack, player.whoAmI);
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "Brimstone", 4);
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddTile(TileID.Bookcases);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}