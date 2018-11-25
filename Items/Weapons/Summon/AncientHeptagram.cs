using Necromancy.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Necromancy.Items.Weapons.Summon
{
	public class AncientHeptagram : HealthCatalyst
	{
        /*
          This is a weird summon item. It doesn't work like vanilla summons or necrotic summons, since it has no minion slot or max life cost.
          The cost of summoning this is a regular health cost that is drained over time. More health means a more powerful summon.
          There is a minimum of 30 health to summon and it stops draining at 1200.
          Because of its nature, this is a child of HealthCatalyst.cs, so the health draining and visual effects of the item are there.
        */

        protected override float Cap
        {
            // maximum health to spend
            get { return 1200f; }
        }

        protected override Color ProgressColor
        {
            // color of the progress text as it reaches 100%
            get { return new Color(0f, 1f, 0.5f); }
        }

        protected override int HealthPerTick
        {
            // how much health is drained per tick
            get { return 4; }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Heptagram");
            Tooltip.SetDefault("Summons a pain elemental to fight for you for 60 seconds" +
                    "\nHold to drain health to make the elemental more powerful");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            item.value = Item.sellPrice(0, 10);
            item.rare = 10;
            item.noUseGraphic = true;
        }

        protected override void OnFull(Player player)
        {
            // when at maximum capacity, interrupt the health draining and create a summon with max power
            OnStop(player, (int)Cap);
        }

        protected override void OnStop(Player player, int healthSpent)
        {
            if (healthSpent > 30)
            {
                // creates the projectile, damage of the summon is proportional to the health spent to create it
                Projectile proj = Projectile.NewProjectileDirect(player.Center, Vector2.Zero, mod.ProjectileType("PainElemental"), healthSpent / 2, 3, player.whoAmI);
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = item;
            }
        }
    }
}