using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Throwing
{
	public class HeartBomb : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heart Bomb");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 15;
            item.crit = 4;
            item.width = 8;
			item.height = 8;
			item.useTime = 15;
			item.useAnimation = 15;
            item.useStyle = 1;
            item.noUseGraphic = true;
            item.knockBack = 2;
			item.rare = 2;
            item.noMelee = true;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("HeartBomb");
            item.shootSpeed = 8f;
            item.consumable = true;
            item.maxStack = 999;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).throwing = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).reloadCost = 30;
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
            recipe.AddIngredient(mod, "BeatingHeart", 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }
}