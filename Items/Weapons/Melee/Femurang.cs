using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Melee
{
	public class Femurang : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Femurang");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 20;
            item.crit = 4;
            item.width = 48;
			item.height = 48;
			item.useTime = 18;
			item.useAnimation = 18;
			item.useStyle = 5;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.knockBack = 3;
			item.value = Item.sellPrice(0, 0, 80, 0);
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Femurang");
			item.shootSpeed = 16f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).melee = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal = 1;
        }

        public override bool CanUseItem(Player player)
        {
            foreach (Projectile proj in Main.projectile)
            {
                if (proj != null && proj.active && proj.owner == player.whoAmI && proj.type == item.shoot)
                {
                    return false;
                }
            }
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(player.Center, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, 2f, -1f);
            proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "BeatingHeart");
            recipe.AddIngredient(ItemID.Bone, 15);
            recipe.AddIngredient(ItemID.DemoniteBar, 10);
            recipe.AddTile(TileID.BoneWelder);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "BeatingHeart");
            recipe.AddIngredient(ItemID.Bone, 15);
            recipe.AddIngredient(ItemID.CrimtaneBar, 10);
            recipe.AddTile(TileID.BoneWelder);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}