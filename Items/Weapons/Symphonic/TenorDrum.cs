using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items.Weapons.Symphonic
{
	public class TenorDrum : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tenor Drum");
            Tooltip.SetDefault("Shoots drum beats in a cross");
            Tooltip.SetDefault("Empowers allies with bonus defense");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 36;
            item.width = 38;
			item.height = 38;
			item.useTime = 3;
            item.useAnimation = 12; // shoots 4 (12/3) times with a 20 frame delay
            item.reuseDelay = 20;
            item.useStyle = 5;
            item.knockBack = 5;
			item.value = Item.sellPrice(0, 3);
			item.rare = 5;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType<TenorDrumBeat>();
			item.shootSpeed = 8f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).symphonic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeCost = 8;
        }
        
        // shoots a projectile in the four cardinal directions
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            player.itemRotation = 0f;
            float numberProjectiles = 4;
            float rotation = MathHelper.ToRadians(90);
            for (int i = 0; i < numberProjectiles; i++) // each iteration rotates by 90 degrees
            {
                Vector2 perturbedSpeed = Vector2.UnitX.RotatedBy(rotation * i) * item.shootSpeed;
                Projectile projectile = Projectile.NewProjectileDirect(position, perturbedSpeed, type, damage, knockBack, player.whoAmI);
                projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            }
            return false;
        }

        public override void UseStyle(Player player)
        {
            player.itemLocation.X -= 16f * player.direction;
            player.itemRotation = 0f;
        }
    }
}