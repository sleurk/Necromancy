using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.NonNecrotic.Throwing
{
	public class BoltAxe : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bolt Axe");
        }

        public override void SetDefaults()
        {
            item.thrown = true;
            item.damage = 62;
            item.width = 36;
            item.height = 34;
            item.useTime = 30;
			item.useAnimation = 30;
            item.useStyle = 1;
            item.noUseGraphic = true;
            item.knockBack = 2;
			item.rare = 8;
            item.noMelee = true;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("BoltAxe");
            item.shootSpeed = 32f;
            item.value = Item.buyPrice(1);
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(player.Center, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }
    }
}