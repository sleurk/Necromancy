using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.NonNecrotic.Radiant
{
	public class Bioluminescence : ModItem
	{
        private float rotation;
        private bool yellow;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bioluminescence");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            // thorium.radiant = true; this is not supported yet because I have not yet spoken to thorium devs about setting up weak references
            item.width = 36;
			item.height = 36;
			item.useTime = 5;
			item.useAnimation = 5;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 2;
            item.value = Item.buyPrice(1);
            item.rare = 5;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
            item.noUseGraphic = true;
			item.shoot = mod.ProjectileType("Bioluminescence");
            item.mana = 4;
			item.shootSpeed = 1f;
            item.prefix = 0;
            rotation = 0f;
            yellow = false;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            // creates the projectile near the mouse, offset by 2.4 radians (fibonnaci angle) to create a spiral effect akin to flowers
            Vector2 mouse = Main.MouseWorld;
            position = mouse + (Vector2.UnitX * 160f).RotatedBy(rotation);
            
            rotation += 2.4f;

            Vector2 vel = new Vector2(speedX, speedY);
            float rotateOffset = Projectiles.Bioluminescence.ROTATION_RATE * -120f;
            vel = (mouse - position).SafeNormalize(Vector2.Zero).RotatedBy(rotateOffset) * vel.Length();
            Projectile proj = Projectile.NewProjectileDirect(position, vel, type, damage, knockBack, player.whoAmI, yellow ? 1f : 0f);
            // ai0 = 1 if yellow, 0 if green, to tell projectile what color to create dusts
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;

            // alternating yellow and green
            yellow = !yellow;
            return false;
        }
    }
}