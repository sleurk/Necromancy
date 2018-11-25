using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Items.Weapons.NonNecrotic.Symphonic
{
	public class Goong : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Goong");
            Tooltip.SetDefault("Empowers allies with bonus damage to enemies covered in goo");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            // thorium.symphonic = true this is not supported yet because I have not yet spoken to thorium devs about setting up weak references
            item.damage = 53;
            item.width = 60;
			item.height = 68;
            item.useStyle = 5;
			item.useTime = 24;
			item.useAnimation = 24;
			item.noMelee = true;
            item.noUseGraphic = true;
			item.knockBack = 0f;
			item.value = Item.buyPrice(1);
			item.rare = 7;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("GoongWave");
            item.shootSpeed = 0f;
            item.prefix = 0;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            // to align sound with use, not strictly necessary
            Main.PlaySound(13, player.Center);
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), item.shoot, damage, knockBack, player.whoAmI, player.Center.X, player.Center.Y);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = item;
            /* 
              Projectile's state was modified after it was created, and Shoot is client-only besides the creation (Projectile.NewProjectileDirect),
              so netUpdate is flagged. This tells the server and other clients to match the projectile's state with the state of the projectile
              on the projectile's owner's client.
             */
            proj.timeLeft += Main.rand.Next(360);
            proj.netUpdate = true;
            return false;
        }
    }
}