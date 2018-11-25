using Microsoft.Xna.Framework;
using Necromancy.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Melee
{
	public class Death : SwipeWeapon
	{
        // this is a child of SwipeWeapon.cs, so the important code is there

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Death");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 250;
            item.width = 68;
			item.height = 64;
            item.knockBack = 4f;
            item.value = Item.sellPrice(0, 12, 75);
			item.rare = 10; // color changed manually in ModifyTooltips
            item.shoot = mod.ProjectileType("DeathSwipe");
            item.shootSpeed = 32f;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal = 8;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).thoriumRarity = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            // shoot scythe projectile
            base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
            
            Projectile proj = Projectile.NewProjectileDirect(player.Center, new Vector2(speedX, speedY) * 0.3f, mod.ProjectileType("DeathSap"), damage / 4, 0f, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = item;
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