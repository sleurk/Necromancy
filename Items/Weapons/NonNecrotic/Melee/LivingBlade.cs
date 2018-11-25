using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.NonNecrotic.Melee
{
    public class LivingBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Living Blade");
        }

        public override void SetDefaults()
        {
            item.melee = true;
            item.damage = 41;
            item.width = 34;
            item.height = 38;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 3;
            item.value = Item.buyPrice(1);
            item.rare = 5;
            item.UseSound = SoundID.Item1;
            item.prefix = 0;
            item.shoot = mod.ProjectileType("LivingBladeShot");
            item.shootSpeed = 16f;
            item.autoReuse = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            // shoots 3 randomly spread (20 degrees) projectiles as well as swinging the sword
            for (int i = 0; i < 3; i++)
            {
                Vector2 vel = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20)) * Main.rand.NextFloat(0.5f, 1f);
                Projectile proj = Projectile.NewProjectileDirect(position, vel, type, damage, knockBack, player.whoAmI);
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            }
            return false;
        }
    }
}
