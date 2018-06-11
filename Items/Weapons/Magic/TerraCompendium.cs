using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Magic
{
	public class TerraCompendium : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Compendium");
            Tooltip.SetDefault("Releases a spirit deathray" +
                "\nDrains more life and does more damage the longer it is sustained");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 80;
            item.crit = 4;
            item.width = 40;
			item.height = 40;
            item.useTime = 25;
            item.channel = true;
			item.useAnimation = 7;
			item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 5;
			item.value = Item.sellPrice(0, 10, 0, 0);
			item.rare = 6;
			item.UseSound = SoundID.Item13;
			item.shoot = mod.ProjectileType("TerraBeam");
			item.shootSpeed = 4f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
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
            recipe.AddIngredient(null, "TrueNecronomicon");
            recipe.AddIngredient(null, "TrueHallowedTome");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TrueBookOfTheDead");
            recipe.AddIngredient(null, "TrueHallowedTome");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}