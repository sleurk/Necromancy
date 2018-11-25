using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items.Weapons.NonNecrotic.Symphonic
{
	public class VerdantViola : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Verdant Viola");
            Tooltip.SetDefault("Shoots an echoing burst of sound" +
                "\nEmpowers allies with bonus damage to glowing enemies");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            // thorium.symphonic = true this is not supported yet because I have not yet spoken to thorium devs about setting up weak references
            item.damage = 45;
            item.width = 54;
            item.height = 20;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.buyPrice(1);
            item.rare = 5;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("ViolaBurst");
            item.shootSpeed = 6f;
            item.prefix = 0;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1f, 0f);
        }
    }
}
