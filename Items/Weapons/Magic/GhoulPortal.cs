using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Magic
{
	public class GhoulPortal : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ghoul Portal");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 31;
            item.width = 28;
			item.height = 30;
			item.useTime = 45;
			item.useAnimation = 45;
            item.useStyle = 5;
            item.knockBack = 0;
			item.value = Item.sellPrice(0, 2);
			item.rare = 6;
            item.noMelee = true;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("GhoulPortal");
            item.shootSpeed = 0f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeCost = 18;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player == Main.LocalPlayer)
            {
                Projectile proj = Projectile.NewProjectileDirect(Main.MouseWorld, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI); // created at cursor
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            }
            return false;
        }
    }
}