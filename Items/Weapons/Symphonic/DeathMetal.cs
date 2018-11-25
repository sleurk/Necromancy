using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Necromancy.Items.Weapons.Melee;

namespace Necromancy.Items.Weapons.Symphonic
{
    // this is a child of SwipeWeapon.cs, so the important code is there
    public class DeathMetal : SwipeWeapon
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Death Metal");
            Tooltip.SetDefault("'End of the line!'" +
                "\nRight click to fire a pulse that brings enemies to you" +
                "\nEmpowers allies with attack speed");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 82;
            item.width = 76;
			item.height = 76;
            item.knockBack = 12f;
            item.useTime = 30;
            item.useAnimation = 30;
            item.value = Item.sellPrice(0, 10);
            item.rare = 5;
            item.shoot = mod.ProjectileType("DeathMetalSwipe");
            item.shootSpeed = 32f;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).melee = false;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).symphonic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).rClickCostOnly = true; // this is to make the weapon only cost life when using the right click
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeCost = 8;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2) // on right click
            {
                Main.PlaySound(SoundID.Item20, player.Center);
                item.autoReuse = true;
                float dist = Vector2.Distance(Main.MouseWorld, player.Center);
                Projectile proj = Projectile.NewProjectileDirect(Main.MouseWorld, -new Vector2(speedX, speedY).SafeNormalize(Vector2.Zero), mod.ProjectileType("DeathMetalShot"), damage / 2, 0f, player.whoAmI, dist);
                proj.timeLeft = (int)(proj.Center - player.Center).Length();
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = item;
                return false;
            }
            else // on left click
            {
                item.autoReuse = false;
                knockBack /= 4;
                return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
            }
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void AddRecipes()
        {
            ThoriumRecipe recipe = new ThoriumRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 15);
            recipe.AddIngredient(ItemID.SoulofMight, 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}