using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Melee
{
    // abstract weapon, only exists as a weapon type
    // represents a weapon that is used like an Arkhalis

	public abstract class SwipeWeapon : ModItem
	{
        public override void SetDefaults()
        {
            item.magic = true;
            item.useStyle = 5;
            item.useTime = 5;
            item.useAnimation = 5;
            item.noUseGraphic = true;
            item.channel = true;
            item.noMelee = true;
			item.autoReuse = false;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).melee = true;
        }

        public override bool CanUseItem(Player player)
        {
            // can only create a new projectile if none exist already
            return base.CanUseItem(player) && player.ownedProjectileCounts[item.shoot] == 0;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), item.shoot, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }
    }
}