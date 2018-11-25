using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Ranged
{
	public class GaussRifle : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gauss Rifle");
            Tooltip.SetDefault("Increases damage with every pierce");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 53;
            item.width = 56;
            item.height = 26;
            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 8;
            item.value = Item.sellPrice(0, 2);
            item.rare = 3;
            item.UseSound = SoundID.Item41;
            item.shoot = mod.ProjectileType("GaussBullet");
            item.shootSpeed = 64f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).ranged = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeCost = 25;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal = 25;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, -5);
        }
    }
}
