using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.NonNecrotic.Magic
{
	public class GlacialTome : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glacial Tome");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 48;
            item.width = 28;
			item.height = 30;
			item.useTime = 5;
			item.useAnimation = 5;
            item.useStyle = 5;
            item.knockBack = 8;
            item.value = Item.buyPrice(1);
            item.rare = 6;
            item.noMelee = true;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("GlacialSpike");
            item.shootSpeed = 128f;
            item.prefix = 0;
            item.mana = 3;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            // recolor direct upgrade to Gore, but a regular magic item
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }
    }
}