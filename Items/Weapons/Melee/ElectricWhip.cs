using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Melee
{
	public class ElectricWhip : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Electric Whip");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 51;
            item.width = 60;
			item.height = 60;
			item.useTime = 18;
			item.useAnimation = 18;
			item.useStyle = 5;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.rare = 8;
            item.knockBack = 5;
            item.value = Item.sellPrice(0, 6);
            item.UseSound = SoundID.Item15;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("ElectricWhip");
			item.shootSpeed = 4f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).melee = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal = 10;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(player.Center, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, 2f, 3f);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }
    }
}