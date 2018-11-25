using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items.Weapons.NonNecrotic.Symphonic
{
	public class BurningBugle : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Burning Bugle");
            Tooltip.SetDefault("Shoots several homing lasers" +
                "\nEmpowers allies with bonus damage to burning enemies");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            // thorium.symphonic = true this is not supported yet because I have not yet spoken to thorium devs about setting up weak references
            item.damage = 56;
            item.width = 54;
            item.height = 30;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.buyPrice(1);
            item.rare = 6;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("BurningDoot");
            item.shootSpeed = 2f;
            item.prefix = 0;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            // moves projectile forward to align with bell
            position += new Vector2(0f, -4f);
            // 3 randomly spread projectiles (10 degrees)
            for (int i = 0; i < 3; i++)
            {
                Vector2 vel = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
                Projectile proj = Projectile.NewProjectileDirect(position + vel * 24f, vel, type, damage, knockBack, player.whoAmI);
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            }
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1f, 0f);
        }
    }
}
