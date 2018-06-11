using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Throwing
{
	public class BeetleFang : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Beetle Fang");
            Tooltip.SetDefault("Throw as fast as you can click");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 61;
            item.crit = 4;
            item.width = 28;
			item.height = 30;
			item.useTime = 2;
			item.useAnimation = 2;
            item.useStyle = 1;
            item.noUseGraphic = true;
            item.knockBack = 2;
			item.rare = 8;
            item.noMelee = true;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
            item.shoot = mod.ProjectileType("BeetleFang");
            item.shootSpeed = 16f;
            item.consumable = true;
            item.maxStack = 999;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).throwing = true;

            item.GetGlobalItem<NecromancyGlobalItem>(mod).reloadCost = 15;
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
            recipe.AddIngredient(ItemID.BeetleHusk, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 100);
            recipe.AddRecipe();
        }
    }
}