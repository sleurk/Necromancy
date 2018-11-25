using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Magic
{
	public class Undeath : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Undeath");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 22;
            item.width = 28;
			item.height = 30;
			item.useTime = 23;
			item.useAnimation = 23;
            item.useStyle = 5;
            item.knockBack = 3;
			item.value = Item.sellPrice(0, 0, 80);
			item.rare = 2;
            item.noMelee = true;
			item.UseSound = SoundID.Item8;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("Spine");
            item.shootSpeed = 1f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeCost = 8;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Book);
            recipe.AddIngredient(ItemID.Bone, 20);
            recipe.AddTile(TileID.Bookcases);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}