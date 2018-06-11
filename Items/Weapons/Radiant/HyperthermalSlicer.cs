using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Radiant
{
	public class HyperthermalSlicer : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hyperthermal Slicer");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 52;
            item.crit = 4;
            item.width = 48;
			item.height = 48;
			item.useTime = 6;
			item.useAnimation = 6;
			item.useStyle = 5;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.knockBack = 3;
			item.value = Item.sellPrice(0, 0, 80, 0);
			item.rare = 4;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("HyperthermalSlicer");
			item.shootSpeed = 4f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).radiant = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).healPower = 3;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(player.Center, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, 2f, -1f);
            proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }
    }
}