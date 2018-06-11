using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Magic
{
	public class BloodyMirror : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloody Mirror");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 30;
			item.width = 22;
			item.height = 22;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 4;
			item.noMelee = true;
			item.knockBack = 5;
			item.value = Item.sellPrice(0, 0, 80, 0);
			item.rare = 4;
			item.UseSound = SoundID.Item9;
			item.shoot = mod.ProjectileType("BloodyMirrorCircle");
			item.shootSpeed = 0f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 100;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            foreach (Projectile p in Main.projectile)
            {
                if (p != null && p.active && p.type == type && p.owner == player.whoAmI)
                {
                    p.Kill();
                }
            }
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            proj.Center = player.Center;
            return false;
        }
    }
}