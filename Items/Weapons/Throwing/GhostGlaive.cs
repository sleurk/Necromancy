using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Necromancy.Projectiles;

namespace Necromancy.Items.Weapons.Throwing
{
	public class GhostGlaive : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ghost Glaive");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 61;
            item.width = 52;
			item.height = 52;
			item.useTime = 12;
			item.useAnimation = 12;
            item.useStyle = 1;
            item.noUseGraphic = true;
            item.knockBack = 2;
			item.rare = 6;
            item.noMelee = true;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("GhostGlaive");
            item.shootSpeed = 16f;
            item.consumable = true;
            item.maxStack = 999;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).throwing = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).reloadCost = 50;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }
    }
}