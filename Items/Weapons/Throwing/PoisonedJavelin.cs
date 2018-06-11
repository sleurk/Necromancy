using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Throwing
{
	public class PoisonedJavelin : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Poisoned Javelin");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 20;
            item.crit = 4;
            item.width = 28;
			item.height = 30;
			item.useTime = 25;
			item.useAnimation = 25;
            item.useStyle = 1;
            item.noUseGraphic = true;
            item.knockBack = 2;
			item.rare = 2;
            item.noMelee = true;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("PoisonedJavelin");
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
            proj.position = player.Center + proj.velocity;
            proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.JungleSpores, 8);
            recipe.AddIngredient(ItemID.Stinger, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 100);
            recipe.AddRecipe();
        }
    }
}