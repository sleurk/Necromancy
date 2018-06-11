using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Melee
{
	public class ButcherCleaver : ModItem
	{
        public bool clockwise = false;


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Butcher's Cleaver");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 15;
            item.crit = 4;
            item.width = 32;
			item.height = 32;
            item.useAnimation = 8;
            item.useTime = 8;
            item.useStyle = 5;
            item.noUseGraphic = true;
            item.channel = true;
            item.noMelee = true;
            item.knockBack = 2f;
            item.value = Item.sellPrice(0, 0, 20, 0);
			item.rare = 2;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("ButcherCleaverSwipe");
            item.shootSpeed = 32f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).melee = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal = 2;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int p = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("ButcherCleaverSwipe"), damage, knockBack, player.whoAmI);
            Main.projectile[p].scale = 1f;
            Main.projectile[p].GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrimtaneBar, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}