using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Necromancy.Projectiles;

namespace Necromancy.Items.Weapons.NonNecrotic.Throwing
{
	public class SearedDagger : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Seared Dagger");
        }

        public override void SetDefaults()
        {
            item.thrown = true;
            item.damage = 65;
            item.width = 18;
			item.height = 36;
            item.useTime = 15;
			item.useAnimation = 15;
            item.useStyle = 1;
            item.noUseGraphic = true;
            item.knockBack = 2;
			item.rare = 6;
            item.noMelee = true;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("SearedDagger");
            item.value = Item.buyPrice(1);
            item.shootSpeed = 8f;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            // throws an even spread of 5 daggers
            for (int i = -2; i <= 2; i++)
            {
                Vector2 vel = new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(i * 5));
                Projectile proj = Projectile.NewProjectileDirect(position, vel, type, damage, knockBack, player.whoAmI);
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            }
            return false;
        }
    }
}