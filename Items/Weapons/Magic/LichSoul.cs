using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Items.Weapons.Magic
{
	public class LichSoul : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lich's Soul");
            Tooltip.SetDefault("Sends a controllable soul that gains power while moving");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 70;
            item.crit = 4;
            item.width = 36;
			item.height = 36;
			item.useTime = 60;
			item.useAnimation = 60;
			item.useStyle = 5;
            item.noUseGraphic = true;
			item.noMelee = true;
			item.knockBack = 5;
            item.channel = true;
			item.value = Item.sellPrice(0, 0, 80, 0);
			item.rare = 6;
			item.UseSound = SoundID.Item20;
			item.shoot = mod.ProjectileType("LichSoul");
			item.shootSpeed = 4f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 100;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }
    }
}