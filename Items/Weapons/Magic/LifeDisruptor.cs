using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Magic
{
	public class LifeDisruptor : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Life Disruptor");
            Tooltip.SetDefault("Shoots weird green energy");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 45;
            item.crit = 4;
            item.width = 64;
			item.height = 64;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 5;
			Item.staff[item.type] = true;
			item.noMelee = true;
			item.knockBack = 1f;
			item.value = Item.sellPrice(0, 0, 80, 0);
			item.rare = 5;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("LifeDisruption");
			item.shootSpeed = 8f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 9;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, Main.rand.NextFloat(0f, 6.28f));
            proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            Mod thorium = ModLoader.GetMod("ThoriumMod");
            if (thorium != null)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(thorium, "StrangePlating", 6);
                recipe.AddIngredient(thorium, "LifeCell");
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}