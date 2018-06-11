using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Radiant
{
	public class PearlkelpWand : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pearl-Kelp Wand");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 27;
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
			item.rare = 2;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("EnergyGreen");
			item.shootSpeed = 3f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).radiant = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 2;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).healPower = 2;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            Mod thorium = ModLoader.GetMod("ThoriumMod");
            if (thorium != null)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(thorium, "Pearl", 6);
                recipe.AddIngredient(thorium, "MarineKelp", 5);
                recipe.AddIngredient(thorium, "MarineKelpSeeds", 5);
                recipe.AddTile(TileID.Anvils);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}