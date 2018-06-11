using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items
{
	public class HarmonyOrb : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orb of Harmony");
            Tooltip.SetDefault("Use life to heal a friend" +
                "\n'Gaze into the Iris.'");
        }

        public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;
            item.useTime = 30;
            item.useAnimation = 10;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useStyle = 5;
            item.maxStack = 1;
            item.UseSound = SoundID.Item9;
			item.value = Item.sellPrice(0, 0, 40, 0);
			item.rare = 8;
            item.channel = true;
            item.shoot = mod.ProjectileType("HarmonyOrb");
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Player target = Necromancy.NearestPlayer(Main.MouseWorld, 100f, player);
            if (target != null)
            {
                Vector2 targetPos = target.position + Vector2.UnitY * -40f;
                Projectile proj = Projectile.NewProjectileDirect(targetPos, Vector2.Zero, item.shoot, 0, 0, player.whoAmI, target.whoAmI);
                proj.netUpdate = true;
            }
            return false;
        }
    }
}