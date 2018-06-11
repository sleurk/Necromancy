using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Symphonic
{
	public class BluesBass : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blues Bass");
            Tooltip.SetDefault("Empowers allies with stacking damage resistance");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 40;
            item.crit = 4;
            item.width = 36;
			item.height = 86;
			item.useTime = 30;
			item.useAnimation = 30;
            item.holdStyle = 3;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 5;
			item.value = Item.sellPrice(0, 0, 80, 0);
			item.rare = 8;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("BluesBlast");
			item.shootSpeed = 16f;
            item.prefix = 0;
            item.UseSound = SoundID.Item94;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).symphonic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 15;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            player.itemRotation = 0f;
            Vector2 speed = new Vector2(speedX, speedY);
            Projectile p = Main.projectile[Projectile.NewProjectile(player.Center + speed, speed, type, damage, knockBack, player.whoAmI)];
            p.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override void HoldStyle(Player player)
        {
            player.itemRotation = 0f;
            player.itemLocation.X -= player.direction * 8f;
            player.itemLocation.Y -= 12f;
        }

        public override void UseStyle(Player player)
        {
            player.itemRotation = 0f;
            player.itemLocation.X -= player.direction * 8f;
            player.itemLocation.Y -= 12f;
        }
    }
}