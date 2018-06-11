using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Magic
{
	public class RingOfPride : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ring of Pride");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 7;
            item.crit = 4;
            item.width = 32;
			item.height = 32;
			item.useTime = 60;
			item.useAnimation = 60;
			item.useStyle = 5;
            item.noUseGraphic = true;
			item.noMelee = true;
			item.knockBack = 5;
			item.value = Item.sellPrice(0, 0, 80, 0);
            item.rare = 1;
            item.UseSound = SoundID.Item8;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("PrideRing");
			item.shootSpeed = 8f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 6;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float r = 12f;
            for (int i = 0; i < 8; i++)
            {
                Vector2 pos = player.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(45) * i) * r;
                HalfVector2 vel = new HalfVector2(speedX, speedY);
                Projectile proj = Projectile.NewProjectileDirect(position, Vector2.Zero, type, damage, knockBack, player.whoAmI, MathHelper.ToRadians(45) * i, vel.PackedValue);
                proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>().shotFrom = item;
                proj.Center = pos;
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SilverBar, 8);
            recipe.AddIngredient(ItemID.Amethyst, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TungstenBar, 8);
            recipe.AddIngredient(ItemID.Amethyst, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}