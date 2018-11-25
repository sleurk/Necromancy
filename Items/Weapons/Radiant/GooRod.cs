using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Radiant
{
	public class GooRod : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Goo Rod");
            Tooltip.SetDefault("Casts a goo cloud to rain goo on your enemies");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 41;
            item.width = 44;
			item.height = 44;
			item.useTime = 23;
			item.useAnimation = 23;
            item.useStyle = 5;
            item.knockBack = 0;
            Item.staff[item.type] = true;
            item.value = Item.buyPrice(1);
            item.rare = 7;
            item.noMelee = true;
			item.UseSound = SoundID.Item20;
			item.autoReuse = false;
            item.shoot = mod.ProjectileType("GooCloudMoving");
            item.shootSpeed = 16f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).radiant = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).healPower = 1;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeCost = 60;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, Main.MouseWorld.X, Main.MouseWorld.Y);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }
    }
}