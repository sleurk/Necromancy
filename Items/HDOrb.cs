using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items
{
    // this file is mostly just to show that you can have multiple items and such in one file
	public class HarmonyOrb : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orb of Harmony");
            Tooltip.SetDefault("Use life to heal an ally" +
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
            item.value = Item.sellPrice(0, 10);
            item.rare = 8;
            item.channel = true;
            item.shoot = mod.ProjectileType("HarmonyOrb");
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Player target = Necromancy.NearestPlayer(Main.MouseWorld, 100f, player); // targets nearest ally within 100px of cursor
            if (target != null)
            {
                Vector2 targetPos = new Vector2(target.Center.X, target.position.Y - 40f); // 40 pixels above the top of the target, centered horizontally
                Projectile proj = Projectile.NewProjectileDirect(targetPos, Vector2.Zero, item.shoot, 0, 0, player.whoAmI, target.whoAmI);
                proj.netUpdate = true;
            }
            return false;
        }
    }

    public class DiscordOrb : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orb of Discord");
            Tooltip.SetDefault("Use life to make an enemy take increased damage" +
                             "\n'There is disquiet in your soul.'");
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
            item.value = Item.sellPrice(0, 10);
            item.rare = 8;
            item.channel = true;
            item.shoot = mod.ProjectileType("DiscordOrb");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            NPC target = Necromancy.NearestNPC(Main.MouseWorld, 100f, false, false); // targets nearest enemy within 100px of cursor
            if (target != null)
            {
                Vector2 targetPos = new Vector2(target.Center.X, target.position.Y - 40f); // 40 pixels above the top of the target, centered horizontally
                Projectile proj = Projectile.NewProjectileDirect(targetPos, Vector2.Zero, item.shoot, 0, 0, player.whoAmI, target.whoAmI);
                proj.netUpdate = true;
            }
            return false;
        }
    }

    // Both have a 2.5% (5% expert) disjoint chance of dropping from Giant Cursed Skull
}