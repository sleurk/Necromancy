using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.NonNecrotic.Melee
{
	public class IceWhip : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Whip");
        }

        public override void SetDefaults()
        {
            item.melee = true;
            item.damage = 43;
            item.width = 60;
			item.height = 60;
			item.useTime = 8;
			item.useAnimation = 8;
			item.useStyle = 5;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.knockBack = 5;
            item.value = Item.buyPrice(1);
            item.rare = 6;
            item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("IceWhip");
			item.shootSpeed = 4f;
            item.prefix = 0;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(player.Center, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, 5f, -1f);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }
    }
}