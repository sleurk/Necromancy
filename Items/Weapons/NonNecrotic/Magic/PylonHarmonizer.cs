using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.NonNecrotic.Magic
{
	public class PylonHarmonizer : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pylon Harmonizer");
            Tooltip.SetDefault("Creates a series of pylons to zap along a line" +
                             "\nRight click to reset active pylons");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 60;
            item.width = 28;
			item.height = 28;
            item.useTime = 25;
            item.channel = true;
			item.useAnimation = 7;
			item.useStyle = 5;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = Item.buyPrice(1);
            item.rare = 8;
			item.UseSound = SoundID.Item13;
            item.shoot = mod.ProjectileType("PylonCreator");
            item.shootSpeed = 4f;
            item.prefix = 0;
            item.mana = 20;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("Pylon")] > 0) return false;
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(player.Center, Vector2.Zero, type, damage, knockBack, player.whoAmI, -1f, -1f);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override bool AltFunctionUse(Player player)
        {
            foreach (Projectile proj in Main.projectile)
            {
                if (proj.owner == player.whoAmI && (proj.type == item.shoot || proj.type == mod.ProjectileType("Pylon")))
                {
                    proj.Kill();
                }
            }
            return false;
        }
    }
}