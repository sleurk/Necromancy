using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Necromancy.Projectiles;

namespace Necromancy.Items.Weapons.Throwing
{
	public class LifeCrystalShard : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Life Crystal Shard");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 9;
            item.width = 28;
			item.height = 30;
			item.useTime = 15;
			item.useAnimation = 15;
            item.useStyle = 1;
            item.noUseGraphic = true;
            item.knockBack = 2;
			item.rare = 1;
            item.noMelee = true;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("LifeCrystalShard");
            item.shootSpeed = 16f;
            item.consumable = true;
            item.maxStack = 999;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).throwing = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).reloadCost = 8;
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
            recipe.AddIngredient(ItemID.LifeCrystal);
            recipe.AddIngredient(mod, "BloodEssence", 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}