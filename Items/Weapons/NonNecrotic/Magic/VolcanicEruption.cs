using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items.Weapons.NonNecrotic.Magic
{
	public class VolcanicEruption : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Volcanic Eruption");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 42;
            item.width = 28;
            item.height = 30;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.noMelee = true;
            item.autoReuse = true;
            item.knockBack = 0f;
            item.value = Item.buyPrice(1);
            item.rare = 6;
            item.UseSound = SoundID.Item20;
            item.shoot = mod.ProjectileType("VolcanicFlame");
            item.shootSpeed = 8f;
            item.prefix = 0;
            item.mana = 8;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            // creates 4-6 projectiles separated by 2 degrees
            int numProj = Main.rand.Next(4, 7);
            for (float i = -(numProj / 2f); i <= numProj / 2f; i++)
            {
                float rotate = MathHelper.ToRadians(2) * i;
                Vector2 vel = new Vector2(speedX, speedY).RotatedBy(rotate);
                Projectile proj = Projectile.NewProjectileDirect(position, vel, type, damage, knockBack, player.whoAmI, Main.rand.NextFloat(-1f, 1f));
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            }
            return false;
        }
    }
}
