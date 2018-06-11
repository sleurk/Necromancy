using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Radiant
{
	public class AncientTendrils : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Tendrils");
            Tooltip.SetDefault("Creates beams of light that lock on to multiple nearby targets");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 20;
            item.crit = 4;
            item.width = 40;
			item.height = 40;
            item.useTime = 25;
            item.channel = true;
			item.useAnimation = 7;
			item.useStyle = 5;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.knockBack = 5;
			item.value = Item.sellPrice(0, 10, 0, 0);
			item.rare = 8;
			item.UseSound = SoundID.Item13;
            item.shoot = mod.ProjectileType("TendrilCluster");
            item.shootSpeed = 4f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).radiant = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).healPower = 1;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(player.Center, Vector2.Zero, type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpectreBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}