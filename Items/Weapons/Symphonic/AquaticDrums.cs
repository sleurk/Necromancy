using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Symphonic
{
	public class AquaticDrums : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquatic Drums");
            Tooltip.SetDefault("Empowers allies with stacking melee damage");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 45;
            item.crit = 4;
            item.width = 48;
			item.height = 48;
			item.useTime = 32;
			item.useAnimation = 32;
			item.useStyle = 5;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.knockBack = 5;
			item.value = Item.sellPrice(0, 0, 80, 0);
			item.rare = 4;
			item.UseSound = SoundID.Item10;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("AquaticDrumBeat");
			item.shootSpeed = 16f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).symphonic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 5;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numProjectiles = 12;
            int degrees = 360 / numProjectiles;
            for (int i = 0; i < numProjectiles; i++)
            {
                Vector2 shootVel = new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(degrees) * i);
                Projectile proj = Projectile.NewProjectileDirect(position, shootVel, type, damage, knockBack, player.whoAmI);
                proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            }
            return false;
        }
    }
}