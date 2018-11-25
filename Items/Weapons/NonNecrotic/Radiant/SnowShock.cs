using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.NonNecrotic.Radiant
{
	public class SnowShock : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snow Shock");
            Tooltip.SetDefault("Creates up to 5 controllable frost balls");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            // thorium.radiant = true; this is not supported yet because I have not yet spoken to thorium devs about setting up weak references
            item.damage = 56;
            item.width = 44;
			item.height = 48;
            item.useTime = 30;
            item.useAnimation = 30;
			item.useStyle = 5;
            item.noUseGraphic = true;
            item.channel = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.knockBack = 0f;
            item.value = Item.buyPrice(1);
            item.rare = 6;
            item.shoot = mod.ProjectileType("SnowShock");
            item.mana = 40;
            item.UseSound = SoundID.Item30;
            item.shootSpeed = 8f;
            item.prefix = 0;
        }

        public override bool CanUseItem(Player player)
        {
            // maximum of 5 projectiles at a time
            return player.ownedProjectileCounts[item.shoot] < 5;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }
    }
}