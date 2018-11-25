using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Magic
{
	public class LightningShock : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning Shock");
            Tooltip.SetDefault("'Unlimited Power?'");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 63;
            
            item.width = 26;
			item.height = 26;
			item.useTime = 5;
			item.useAnimation = 5;
			item.useStyle = 5;
            item.noUseGraphic = true;
			item.noMelee = true;
			item.knockBack = 5;
			item.value = Item.buyPrice(1);
			item.rare = 8;
			item.UseSound = SoundID.Item12;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("LightningShock");
			item.shootSpeed = 128f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeCost = 7;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float dist = (Main.MouseWorld - player.Center).Length() / 3f * Main.rand.NextFloat(0.9f, 1.1f);
            Vector2 dir = new Vector2(speedX, speedY).SafeNormalize(Vector2.UnitX);
            position += dir * 12f;
            dir *= dist;
            Projectile proj = Projectile.NewProjectileDirect(position, dir, type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }
    }
}