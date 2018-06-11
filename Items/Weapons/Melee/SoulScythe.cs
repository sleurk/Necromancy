using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Melee
{
	public class SoulScythe : ModItem
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Scythe");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 67;
            item.crit = 4;
            item.width = 74;
			item.height = 68;
            item.useAnimation = 12;
            item.useTime = 12;
            item.useStyle = 5;
            item.noUseGraphic = true;
            item.channel = true;
            item.noMelee = true;
            item.knockBack = 4f;
            item.value = Item.sellPrice(0, 10, 0, 0);
			item.rare = 8;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("SoulScytheSwipe");
            item.shootSpeed = 32f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).melee = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal = 3;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int p = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
            Main.projectile[p].scale = 1f;
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Ectoplasm, 10);
            recipe.AddIngredient(ItemID.SpookyWood, 100);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}