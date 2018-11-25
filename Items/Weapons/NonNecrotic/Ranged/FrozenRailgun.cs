using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items.Weapons.NonNecrotic.Ranged
{
	public class FrozenRailgun : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frozen Railgun");
        }

        public override void SetDefaults()
        {
            item.ranged = true;
            item.damage = 310;
            item.width = 82;
            item.height = 26;
            item.useTime = 80;
            item.useAnimation = 80;
            item.useStyle = 5;
            item.noMelee = true;
            item.value = Item.buyPrice(1);
            item.rare = 6;
            item.UseSound = SoundID.Item41;
            item.shoot = 10;
            item.shootSpeed = 120f;
            item.prefix = 0;
            item.useAmmo = AmmoID.Bullet;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.Bullet) type = mod.ProjectileType("FrozenShot");
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12, 4);
        }
    }
}
