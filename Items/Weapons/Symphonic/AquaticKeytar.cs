using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Symphonic
{
	public class AquaticKeytar : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquatic Keytar");
            Tooltip.SetDefault("Empowers allies with stacking magic damage");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 15;
            item.crit = 4;
            item.width = 48;
			item.height = 48;
			item.useTime = 8;
			item.useAnimation = 8;
			item.useStyle = 5;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.knockBack = 5;
			item.value = Item.sellPrice(0, 0, 80, 0);
			item.rare = 4;
			item.UseSound = SoundID.Item15;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("AquaticKeytarTune");
			item.shootSpeed = 16f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).symphonic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 5;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(player.Center, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            proj = Projectile.NewProjectileDirect(player.Center, new Vector2(-speedX, -speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }
    }
}