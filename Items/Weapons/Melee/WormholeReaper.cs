using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Melee
{
	public class WormholeReaper : SwipeWeapon
	{
        // this is a child of SwipeWeapon.cs, so the important code is there

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wormhole Reaper");
            Tooltip.SetDefault("'Send their souls to the void and back'");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 82;
            item.width = 38;
			item.height = 38;
            item.knockBack = 4f;
            item.value = Item.sellPrice(0, 10);
            item.rare = 10;
            item.shoot = mod.ProjectileType("WormholeScytheSwipe");
            item.shootSpeed = 32f;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal = 7;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            // shoot scythe projectile
            base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);

            // shoot secondary projectile
            foreach (Projectile p in Main.projectile)
            {
                // kill any existing secondary projectiles
                if (p != null && p.active && p.owner == player.whoAmI && p.type == mod.ProjectileType("WormholeScytheShot")) p.Kill();
            }
            Projectile proj = Projectile.NewProjectileDirect(player.Center, new Vector2(speedX, speedY) * 0.3f, mod.ProjectileType("WormholeScytheShot"), damage / 4, 0f, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "FragmentWormhole", 18);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}