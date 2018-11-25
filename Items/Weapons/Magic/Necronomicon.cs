using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Magic
{
	public class Necronomicon : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Necronomicon");
            Tooltip.SetDefault("Creates a high-damage projectile after a small delay at the cursor");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 52;
            item.width = 28;
			item.height = 30;
			item.useTime = 3;
            item.useAnimation = 3;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 5;
            item.value = Item.sellPrice(0, 1);
			item.rare = 3;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("NecroPulse");
			item.shootSpeed = 0f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeCost = 3;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = Main.MouseWorld; // create projectile at cursor
            Projectile proj = Projectile.NewProjectileDirect(position, Vector2.Zero, type, damage, 0f, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "Rot");
            recipe.AddIngredient(mod, "Nature");
            recipe.AddIngredient(mod, "Undeath");
            recipe.AddIngredient(mod, "Hellfire");
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}