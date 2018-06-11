using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Magic
{
	public class RingOfEnvy : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ring of Envy");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 15;
            item.crit = 4;
            item.width = 32;
			item.height = 32;
			item.useTime = 36;
			item.useAnimation = 36;
			item.useStyle = 5;
            item.noUseGraphic = true;
			item.noMelee = true;
			item.knockBack = 5;
			item.value = Item.sellPrice(0, 0, 80, 0);
			item.rare = 1;
			item.UseSound = SoundID.Item8;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("EnvyRing");
			item.shootSpeed = 32f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 6;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                float shootSpeed = (Main.MouseWorld - player.Center).Length() / 22.9259f;
                position = player.Center;
                Vector2 speed = new Vector2(speedX, speedY);
                Projectile proj1 = Projectile.NewProjectileDirect(position, speed.RotatedBy(MathHelper.ToRadians(90)).SafeNormalize(Vector2.UnitX * player.direction) * shootSpeed, type, damage, knockBack, player.whoAmI, -1f);
                proj1.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;

                Projectile proj2 = Projectile.NewProjectileDirect(position, speed.RotatedBy(MathHelper.ToRadians(-90)).SafeNormalize(Vector2.UnitX * player.direction) * shootSpeed, type, damage, knockBack, player.whoAmI, 1f);
                proj2.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;

                proj1.ai[1] = proj2.whoAmI;
                proj2.ai[1] = proj1.whoAmI;
                proj1.netUpdate = true;
                proj2.netUpdate = true;
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SilverBar, 8);
            recipe.AddIngredient(ItemID.Emerald, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TungstenBar, 8);
            recipe.AddIngredient(ItemID.Emerald, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}