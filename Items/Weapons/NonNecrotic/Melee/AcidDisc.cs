using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.NonNecrotic.Melee
{
	public class AcidDisc : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acid Disc");
        }

        public override void SetDefaults()
        {
            item.melee = true;
            item.damage = 50;
            item.width = 64;
			item.height = 64;
			item.useTime = 18;
			item.useAnimation = 18;
			item.useStyle = 5;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.knockBack = 3;
			item.value = Item.buyPrice(1);
			item.rare = 7;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("AcidDisc");
			item.shootSpeed = 24f;
            item.prefix = 0;
        }

        public override bool CanUseItem(Player player)
        {
            // can only use if there are no existing projectiles from this item
            return player.ownedProjectileCounts[item.shoot] == 0;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(player.Center, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }
    }
}