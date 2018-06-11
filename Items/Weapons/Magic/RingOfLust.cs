using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Magic
{
	public class RingOfLust : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ring of Lust");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 15;
            item.crit = 4;
            item.width = 32;
			item.height = 32;
			item.useTime = 10;
			item.useAnimation = 30;
			item.useStyle = 5;
            item.noUseGraphic = true;
			item.noMelee = true;
			item.knockBack = 5;
			item.value = Item.sellPrice(0, 0, 80, 0);
            item.rare = 1;
            item.UseSound = SoundID.Item8;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("LustRing");
			item.shootSpeed = 16f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 6;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = player.Center + new Vector2(speedX, speedY).SafeNormalize(Vector2.UnitX * player.direction).RotatedByRandom(MathHelper.ToRadians(30)) * 500f;
            Vector2 vel = player.Center - position;
            vel.Normalize();
            vel *= item.shootSpeed;
            Projectile proj = Projectile.NewProjectileDirect(position, vel, type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>().shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            Mod thorium = ModLoader.GetMod("ThoriumMod");
            if (thorium == null)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.SilverBar, 8);
                recipe.AddIngredient(ItemID.LifeCrystal, 1);
                recipe.AddTile(TileID.Anvils);
                recipe.SetResult(this);
                recipe.AddRecipe();

                recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.TungstenBar, 8);
                recipe.AddIngredient(ItemID.LifeCrystal, 1);
                recipe.AddTile(TileID.Anvils);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            else
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.SilverBar, 8);
                recipe.AddIngredient(thorium, "Opal", 1);
                recipe.AddTile(TileID.Anvils);
                recipe.SetResult(this);
                recipe.AddRecipe();

                recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.TungstenBar, 8);
                recipe.AddIngredient(thorium, "Opal", 1);
                recipe.AddTile(TileID.Anvils);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}