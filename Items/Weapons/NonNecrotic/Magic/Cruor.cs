using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.NonNecrotic.Magic
{
	public class Cruor : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cruor");
            Tooltip.SetDefault("Creates an X of damage around the player");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 41;
            item.width = 46;
			item.height = 46;
            item.useTime = 20;
            item.useAnimation = 20;
			item.useStyle = 5;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.knockBack = 0;
            item.value = Item.buyPrice(1);
            item.rare = 4;
            item.shoot = mod.ProjectileType("Cruor");
            item.shootSpeed = 0f;
            item.prefix = 0;
            item.mana = 6;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            // play sound manually to prevent it from desyncing with the actual weapon use, not strictly necessary
            Main.PlaySound(SoundID.Item27, player.Center);
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }
    }
}