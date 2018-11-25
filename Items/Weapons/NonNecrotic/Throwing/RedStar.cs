using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Necromancy.Projectiles;

namespace Necromancy.Items.Weapons.NonNecrotic.Throwing
{
	public class RedStar : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red Star");
        }

        public override void SetDefaults()
        {
            item.thrown = true;
            item.damage = 32;
            item.width = 22;
			item.height = 22;
			item.useTime = 10;
			item.useAnimation = 10;
            item.useStyle = 1;
            item.noUseGraphic = true;
            item.knockBack = 2;
			item.rare = 4;
            item.noMelee = true;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("RedStar");
            item.value = Item.buyPrice(1);
            item.shootSpeed = 4f;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }
    }
}