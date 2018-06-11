using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Summon
{
	public class AncientHeptagram : ModItem
    {
        private int healthSpent = 0;
        private int use = 0; // to keep track of if the button is held down or not
        private bool useOld = false; // to keep track of if the button was held down last frame

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Heptagram");
            Tooltip.SetDefault("Summons a pain elemental to fight for you for 60 seconds" +
                    "\nHold to drain health to make the elemental more powerful");
        }

        public override void SetDefaults()
        {
            item.width = 66;
			item.height = 66;
            item.useAnimation = 3;
            item.useTime = 3;
            item.useStyle = 4;
            item.noMelee = true;
			item.rare = 10;
            item.UseSound = SoundID.Item3;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.value = Item.sellPrice(0, 10, 0, 0);
        }

        public override bool UseItem(Player player)
        {
            item.autoReuse = true;
            useOld = true;
            use = 3;
            healthSpent += 5;
            Necromancy.DrainLife(player, 5);
            if (healthSpent >= 500)
            {
                healthSpent = 0;
                use = 0;
                useOld = false;
                item.autoReuse = false;
            }
            return true;
        }

        public override void HoldItem(Player player)
        {
            if (use > 0)
            {
                use -= 1;
            }
            else if (useOld)
            {
                if (healthSpent > 30)
                {
                    Projectile proj = Projectile.NewProjectileDirect(player.Center, Vector2.Zero, mod.ProjectileType("PainElemental"), healthSpent, 3, player.whoAmI);
                    proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>().shotFrom = item;
                    proj.netUpdate = true;
                }
                healthSpent = 0;
                useOld = false;
            }
        }
	}
}
