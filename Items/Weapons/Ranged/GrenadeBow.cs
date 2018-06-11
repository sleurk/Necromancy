using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Ranged
{
	public class GrenadeBow : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grenade Bow");
            Tooltip.SetDefault("Increase blast radius with airtime");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 53;
            item.crit = 4;
            item.width = 60;
            item.height = 26;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 5;
            item.autoReuse = true;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 4;
            item.UseSound = SoundID.Item5;
            item.shoot = mod.ProjectileType("Grenade");
            item.shootSpeed = 18f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).ranged = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 30;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal = 30;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
        }
    }
}
