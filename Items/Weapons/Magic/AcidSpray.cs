using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Magic
{
	public class AcidSpray : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acid Spray");
            Tooltip.SetDefault("Shoots jets of acid, applying cursed flame and ichor");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 67;
            item.width = 50;
            item.height = 50;
            item.useStyle = 5;
            item.useAnimation = 12;
            item.useTime = 6;
            item.knockBack = 3f;
            item.shootSpeed = 12.5f;
            item.UseSound = SoundID.Item13;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.rare = 7;
            item.value = Item.sellPrice(0, 5);
            item.autoReuse = true;
			item.shoot = mod.ProjectileType("AcidSpray");
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeCost = 8;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position += new Vector2(speedX, speedY); // to start from edge of staff
            int numberProjectiles = 1 + Main.rand.Next(2); // 1-2
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5)); // 5 degree spread
                float scale = 1f - (Main.rand.NextFloat() * .3f); // 70% - 100% velocity
                perturbedSpeed = perturbedSpeed * scale; 
                Projectile proj = Projectile.NewProjectileDirect(position, perturbedSpeed, type, damage, knockBack, player.whoAmI);
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            }
            return false;
        }

        // Drops from plantera/boss bag
    }
}