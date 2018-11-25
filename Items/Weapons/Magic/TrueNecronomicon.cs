using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Magic
{
	public class TrueNecronomicon : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Necronomicon");
            Tooltip.SetDefault("Angers lasting spirits around you after a delay");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 72;
            item.width = 28;
			item.height = 30;
			item.useTime = 3;
			item.useAnimation = 3;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 5;
            item.value = Item.sellPrice(0, 10);
            item.rare = 6;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("TrueNecroPulse");
			item.shootSpeed = 0f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeCost = 7;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = Main.MouseWorld; // starts at cursor
            Projectile proj = Projectile.NewProjectileDirect(position, Vector2.Zero, type, damage, 0f, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "Necronomicon");
            recipe.AddIngredient(mod, "BrokenHeroTome");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}