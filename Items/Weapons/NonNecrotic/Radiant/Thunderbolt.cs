using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.NonNecrotic.Radiant
{
	public class Thunderbolt : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thunderbolt");
            Tooltip.SetDefault("Charge and release to create a high-power healing pulse");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            // thorium.radiant = true; this is not supported yet because I have not yet spoken to thorium devs about setting up weak references
            item.width = 28;
			item.height = 52;
            item.useTime = 25;
            item.channel = true;
			item.useAnimation = 7;
			item.useStyle = 5;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.knockBack = 5;
            item.value = Item.buyPrice(1);
            item.rare = 8;
			item.UseSound = SoundID.Item13;
            item.shoot = mod.ProjectileType("ThunderCharge");
            item.mana = 60;
            item.shootSpeed = 1f;
            item.prefix = 0;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(player.Center, Vector2.Zero, type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }
    }
}