using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.NonNecrotic.Melee
{
	public class Magmatica : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magmatica");
        }

        public override void SetDefaults()
        {
            item.melee = true;
            item.damage = 43;
            item.width = 68;
			item.height = 48;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 5;
			item.knockBack = 6;
            item.channel = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
			item.useTurn = false;﻿
            item.noMelee = true;
			item.rare = 8;
            item.value = Item.buyPrice(1);
            item.UseSound = SoundID.Item1;
			item.shoot = mod.ProjectileType("Magmatica");
			item.shootSpeed = 32f;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }
    }
}
