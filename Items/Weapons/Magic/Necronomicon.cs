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
            Tooltip.SetDefault("Angers spirits around you after a delay");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 52;
            item.crit = 4;
            item.width = 28;
			item.height = 30;
			item.useTime = 2;
            item.useAnimation = 2;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 5;
            item.value = Item.sellPrice(0, 10, 0, 0);
			item.rare = 3;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("NecroPulse");
			item.shootSpeed = 0f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 2;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                position = Main.MouseWorld;
                Projectile proj = Projectile.NewProjectileDirect(position, Vector2.Zero, type, damage, 0f, player.whoAmI);
                proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>().shotFrom = item;
                proj.Center = position;
                proj.netUpdate = true;
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Rot");
            recipe.AddIngredient(null, "Nature");
            recipe.AddIngredient(null, "Undeath");
            recipe.AddIngredient(null, "Hellfire");
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}