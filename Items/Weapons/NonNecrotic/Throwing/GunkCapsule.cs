using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Necromancy.Projectiles;

namespace Necromancy.Items.Weapons.NonNecrotic.Throwing
{
	public class GunkCapsule : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gunk Capsule");
        }

        public override void SetDefaults()
        {
            item.thrown = true;
            item.damage = 72;
            item.width = 34;
			item.height = 34;
			item.useTime = 20;
			item.useAnimation = 20;
            item.useStyle = 1;
            item.noUseGraphic = true;
            item.knockBack = 2;
			item.rare = 7;
            item.noMelee = true;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("GunkCapsule");
            item.value = Item.buyPrice(1);
            item.shootSpeed = 16f;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, 0f, Main.rand.Next(6) + 1);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }
    }
}