using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Melee
{
	public class DevilTrident : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Devil's Trident");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 31;
            item.width = 48;
			item.height = 48;
			item.useTime = 20;
			item.useAnimation = 22;
			item.useStyle = 5;
			item.knockBack = 6;
            item.channel = true;
            item.noUseGraphic = true;
			item.useTurn = false;﻿
            item.noMelee = true;
            item.rare = 6;
            item.value = Item.buyPrice(1);
            item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("DevilTrident");
			item.shootSpeed = 2.5f;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).melee = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal = 3;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            // creates spear and shoots 3 other projectiles separated by 10 degrees each

            // spear
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = item;

            // other projectiles
            proj = Projectile.NewProjectileDirect(position, 5f * new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(-10)), mod.ProjectileType("TridentBlast"), damage, knockBack, player.whoAmI, -1);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = item;
            proj = Projectile.NewProjectileDirect(position, 5f * new Vector2(speedX, speedY), mod.ProjectileType("TridentBlast"), damage, knockBack, player.whoAmI, 0);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = item;
            proj = Projectile.NewProjectileDirect(position, 5f * new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(10)), mod.ProjectileType("TridentBlast"), damage, knockBack, player.whoAmI, 1);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = item;
            // ai0 = projectile's relative direction, determines how it curves
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            // Ensures no more than one spear can be thrown out, use this when using autoReuse
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
    }
}
