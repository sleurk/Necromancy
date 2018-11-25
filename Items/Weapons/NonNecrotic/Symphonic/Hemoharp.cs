using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Items.Weapons.NonNecrotic.Symphonic
{
	public class Hemoharp : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hemoharp");
            Tooltip.SetDefault("Empowers allies with bonus damage to wounded enemies");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            // thorium.symphonic = true this is not supported yet because I have not yet spoken to thorium devs about setting up weak references
            item.damage = 31;
            item.width = 42;
			item.height = 46;
            item.useStyle = 5;
			item.useTime = 15;
			item.useAnimation = 15;
            item.noUseGraphic = true;
			item.noMelee = true;
			item.knockBack = 5;
			item.value = Item.buyPrice(1);
			item.rare = 4; 
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("HemoharpNote");
            item.shootSpeed = 8f;
            item.prefix = 0;
            item.UseSound = SoundID.Item26;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            // shoots 20 evenly spaced projectiles in a ring around the player
            for (int i = 0; i < 20; i++)
            {
                Vector2 pos = player.Center + Vector2.UnitX.RotatedBy(MathHelper.TwoPi / 20f * i) * item.shootSpeed * 4f;
                Vector2 vel = (pos - player.Center) / 4f;
                Projectile proj = Projectile.NewProjectileDirect(pos, vel, item.shoot, damage, knockBack, player.whoAmI);
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = item;
            }
            return false;
        }
    }
}