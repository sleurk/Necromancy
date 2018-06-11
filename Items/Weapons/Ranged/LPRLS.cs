using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Necromancy.Items.Weapons.Ranged
{
	public class LPRLS : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("L.P.R.L.S.");
            Tooltip.SetDefault("Fires a spread of 4 rockets");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.RocketLauncher);
            item.damage = 40;
            item.crit = 4;
            item.width = 90;
            item.height = 42;
            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.sellPrice(0, 4, 0, 0);
            item.rare = 7;
            item.UseSound = refItem.UseSound;
            item.autoReuse = true;
			item.shoot = mod.ProjectileType("LifeRocket");
            item.shootSpeed = 18f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).ranged = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 60;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal = 15;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 4;
			for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
				float scale = 1f - (Main.rand.NextFloat() * .3f);
				perturbedSpeed = perturbedSpeed * scale; 
				Projectile proj = Projectile.NewProjectileDirect(position, perturbedSpeed, type, damage, knockBack, player.whoAmI);
                proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            }
			return false;
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "LivingHeart");
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-30, 0);
        }
    }
}
 