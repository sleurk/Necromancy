using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Necromancy.Items
{
    // this is a child of SwipeWeapon.cs, so the important code is there
    // this will drain the player's life as long as they hold click until they drain 800 life, at which point it spawns an item
    public class GreaterHealthCatalyst : HealthCatalyst
	{
        protected override float Cap
        {
            // maximum health to drain
            get { return 800f; }
        }

        protected override Color ProgressColor
        {
            // color for progress text to be as it approaches 100%
            get { return new Color(1f, 0.5f, 0f); }
        }

        protected override int HealthPerTick
        {
            // how much health is drained per tick
            get { return 2; }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Greater Health Catalyst");
            Tooltip.SetDefault("Hold to drain life" +
                    "\nDrain " + (int)Cap + " life to create a Living Heart");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            item.value = Item.sellPrice(0, 3);
            item.rare = 7;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 6);
            recipe.AddIngredient(ItemID.BottledHoney, 10);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }

        protected override void OnFull(Player player)
        {
            // when the player has drained 800 life
            Item.NewItem(player.Center, mod.ItemType("LivingHeart"), 1, true, 0, true);
        }
	}
}