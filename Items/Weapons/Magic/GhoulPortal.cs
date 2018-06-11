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
            item.damage = 43;
            item.crit = 4;
            item.width = 28;
			item.height = 30;
			item.useTime = 45;
			item.useAnimation = 45;
            item.useStyle = 5;
            item.knockBack = 0;
			item.value = Item.sellPrice(0, 2, 0, 0);
			item.rare = 6;
            item.noMelee = true;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("GhoulPortal");
            item.shootSpeed = 4f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 15;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player == Main.LocalPlayer)
            {
                Projectile proj = Projectile.NewProjectileDirect(Main.MouseWorld, Vector2.Zero, type, damage, knockBack, player.whoAmI);
                proj.Center = Main.MouseWorld;
                proj.netUpdate = true;
                proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            }
            return false;
        }
    }
}