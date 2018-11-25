using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.NonNecrotic.Radiant
{
	public class Ooze : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ooze");
            Tooltip.SetDefault("Shoots a glob of ooze that loosely sticks to hit enemies");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            // thorium.radiant = true; this is not supported yet because I have not yet spoken to thorium devs about setting up weak references
            item.damage = 56;
            item.width = 38;
			item.height = 58;
            item.useTime = 30;
            item.useAnimation = 30;
			item.useStyle = 5;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.knockBack = 0f;
            item.value = Item.buyPrice(1);
            item.rare = 7;
            item.shoot = mod.ProjectileType("Ooze");
            item.mana = 38;
            item.UseSound = SoundID.Item1;
            item.shootSpeed = 16f;
            item.prefix = 0;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }
    }
}