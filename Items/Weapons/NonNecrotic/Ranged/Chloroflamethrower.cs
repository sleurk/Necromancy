using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items.Weapons.NonNecrotic.Ranged
{
	public class Chloroflamethrower : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chloroflamethrower");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Flamethrower);
            item.damage = 51;
            item.width = 64;
            item.height = 18;
            item.value = Item.buyPrice(1);
            item.rare = 5;
            item.shoot = mod.ProjectileType("Chloroflame");
            item.shootSpeed *= 1.5f;
            item.prefix = 0;
            item.useAmmo = AmmoID.Gel;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position += new Vector2(speedX, speedY) * 5f;
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, -5);
        }

        public override bool ConsumeAmmo(Player player)
        {
            return player.itemAnimation > player.itemAnimationMax - item.useTime;
        }
    }
}
