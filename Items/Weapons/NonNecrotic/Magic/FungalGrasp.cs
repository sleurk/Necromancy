using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items.Weapons.NonNecrotic.Magic
{
	public class FungalGrasp : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fungal Grasp");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 30;
            item.width = 28;
            item.height = 30;
            item.useTime = 6;
            item.useAnimation = 6;
            item.useStyle = 5;
            item.noMelee = true;
            item.autoReuse = true;
            item.knockBack = 8;
            item.value = Item.buyPrice(1);
            item.rare = 5;
            item.UseSound = SoundID.Item13;
            item.shoot = mod.ProjectileType("FungalGrasp");
            item.shootSpeed = 1f;
            item.prefix = 0;
            item.mana = 6;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            // this weapon is functionally and visually similar to the Shadowflame Hex Doll
            float rotate = Main.rand.NextFloat(-1f, 1f);
            Vector2 shoot = new Vector2(speedX, speedY);
            position += shoot * 16f;
            shoot = shoot.RotatedByRandom(MathHelper.ToRadians(20f * rotate));
            Projectile proj = Projectile.NewProjectileDirect(position, shoot, type, damage, knockBack, player.whoAmI, Main.rand.NextFloat(-1f, 1f));
            // ai0 = rotation speed factor
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }
    }
}
