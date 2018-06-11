using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Magic
{
	public class RingOfWrath : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ring of Wrath");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 15;
            item.crit = 4;
            item.width = 32;
			item.height = 32;
			item.useTime = 5;
			item.useAnimation = 20;
            item.reuseDelay = 45;
			item.useStyle = 5;
            item.noUseGraphic = true;
			item.noMelee = true;
			item.knockBack = 5;
			item.value = Item.sellPrice(0, 0, 80, 0);
            item.rare = 1;
            item.UseSound = SoundID.Item8;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("WrathRing");
			item.shootSpeed = 8f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 6;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 vel = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(360f));
            Projectile proj = Projectile.NewProjectileDirect(position, vel, type, damage, 16f, player.whoAmI);
            proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>().shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SilverBar, 8);
            recipe.AddIngredient(ItemID.Ruby, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TungstenBar, 8);
            recipe.AddIngredient(ItemID.Ruby, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}