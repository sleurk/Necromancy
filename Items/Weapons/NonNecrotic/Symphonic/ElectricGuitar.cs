using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Items.Weapons.NonNecrotic.Symphonic
{
	public class ElectricGuitar : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Electric Guitar");
            Tooltip.SetDefault("Empowers allies with bonus damage to shocked enemies");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            // thorium.symphonic = true this is not supported yet because I have not yet spoken to thorium devs about setting up weak references
            item.damage = 67;
            item.width = 70;
			item.height = 58;
            item.useStyle = 5;
			item.useTime = 12;
			item.useAnimation = 12;
			item.noMelee = true;
			item.knockBack = 0f;
			item.value = Item.buyPrice(1);
			item.rare = 8;
			item.autoReuse = true;
            item.UseSound = SoundID.Item47;
            item.shoot = mod.ProjectileType("ElectricBall");
            item.shootSpeed = 24f;
            item.prefix = 0;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), item.shoot, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = item;
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-24f, 0f);
        }
    }
}