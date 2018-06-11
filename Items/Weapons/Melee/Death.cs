using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Melee
{
	public class Death : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Death");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 101;
            item.crit = 4;
            item.width = 68;
			item.height = 64;
            item.useAnimation = 8;
            item.useTime = 8;
            item.useStyle = 5;
            item.noUseGraphic = true;
            item.channel = true;
            item.noMelee = true;
            item.knockBack = 4f;
            item.value = Item.sellPrice(0, 10, 0, 0);
			item.rare = 10;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("DeathSwipe");
            item.shootSpeed = 32f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).melee = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal = 8;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).thoriumRarity = true;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int p = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DeathSwipe"), damage, knockBack, player.whoAmI);
            Main.projectile[p].scale = 1f;
            Main.projectile[p].GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            Mod thorium = ModLoader.GetMod("ThoriumMod");
            if (thorium != null)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(thorium, "InfernoEssence");
                recipe.AddIngredient(thorium, "DeathEssence");
                recipe.AddIngredient(thorium, "OceanEssence");
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}