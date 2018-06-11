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
            item.crit = 4;
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
			item.value = Item.sellPrice(0, 0, 80, 0);
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("AcidSpray");
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 4;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position += new Vector2(speedX, speedY);
            int numberProjectiles = 1 + Main.rand.Next(2);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
                float scale = 1f - (Main.rand.NextFloat() * .3f);
                perturbedSpeed = perturbedSpeed * scale; 
                Projectile proj = Projectile.NewProjectileDirect(position, perturbedSpeed, type, damage, knockBack, player.whoAmI);
                proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            }
            return false;
        }
    }
}