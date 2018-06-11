using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Radiant
{
	public class WebrootWand : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Web-Root Wand");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 8;
            item.crit = 4;
            item.width = 40;
			item.height = 40;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 5;
			Item.staff[item.type] = true;
			item.noMelee = true;
			item.knockBack = 5;
			item.value = Item.sellPrice(0, 0, 80, 0);
			item.rare = 1;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("EnergyOrange");
			item.shootSpeed = 3f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).radiant = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 1;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).healPower = 1;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cobweb, 6);
            recipe.AddIngredient(ItemID.Blinkroot, 5);
            recipe.AddIngredient(ItemID.BlinkrootSeeds, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}