using Microsoft.Xna.Framework;
using Necromancy.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Magic
{
	public class Hunger : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hunger");
            Tooltip.SetDefault("'These spirits crave blood'");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 78;
            item.width = 28;
			item.height = 30;
            item.useTime = 2;
            item.useAnimation = 10; // shoots 5 (10/2) projectiles in 10 frames, with a cooldown of 35 frames
            item.reuseDelay = 35;
            item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 5;
            item.value = Item.sellPrice(0, 10);
			item.rare = 10;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("HungerOrb");
			item.shootSpeed = 16f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeCost = 20;
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
            recipe.AddIngredient(mod, "FragmentWormhole", 18);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}