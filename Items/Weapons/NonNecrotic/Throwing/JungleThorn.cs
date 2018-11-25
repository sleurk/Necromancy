using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Necromancy.Projectiles;

namespace Necromancy.Items.Weapons.NonNecrotic.Throwing
{
	public class JungleThorn : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jungle Thorn");
            Tooltip.SetDefault("Throws an erratically growing thorn");
        }

        public override void SetDefaults()
        {
            item.thrown = true;
            item.damage = 52;
            item.width = 14;
			item.height = 22;
			item.useTime = 30;
			item.useAnimation = 30;
            item.useStyle = 1;
            item.noUseGraphic = true;
            item.knockBack = 2;
			item.rare = 5;
            item.noMelee = true;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("JungleThorn");
            item.value = Item.buyPrice(1);
            item.shootSpeed = 1f;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, 25);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }
    }
}