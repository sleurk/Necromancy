using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.NonNecrotic.Ranged
{
	public class ToxicCannon : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toxic Cannon");
        }

        public override void SetDefaults()
        {
            item.ranged = true;
            item.damage = 53;
            item.width = 36;
            item.height = 28;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 5;
            item.autoReuse = true;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = Item.buyPrice(1);
            item.rare = 7;
            item.UseSound = SoundID.Item5;
            item.shoot = mod.ProjectileType("Toxin");
            item.shootSpeed = 6f;
            item.prefix = 0;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            // 5 projectiles randomly spread (10 degrees)
            for (int i = 0; i < 5; i++)
            {
                Vector2 vel = new Vector2(speedX, speedY);
                position += vel * 5f;
                // moved forward to align with gun
                vel = vel.RotatedByRandom(MathHelper.ToRadians(10));
                Projectile proj = Projectile.NewProjectileDirect(position, vel, type, damage, knockBack, player.whoAmI);
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            }
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4f, 0f);
        }
    }
}
