using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Necromancy.Items
{
	public class HealthCatalyst : ModItem
	{
        /*
          This is an item and a parent of several other items that act the same way.
          This item and its children work like this:
            The player holds left click and the item constantly drains health. 
            Text will appear on the player showing how much health they have drained so far compared to the maximum.
            When the maximum amount of health is drained, something happens and its tops draining health.
          This item specifically will create an item when it is finished.
        */
        private int healthSpent = 0;
        private int use = 0; // to keep track of if the button is held down or not
        private int timer = 0;

        protected virtual float Cap
        {
            // maximum health drained
            get { return 400f; }
        }

        protected virtual Color ProgressColor
        {
            // color for progress text to be as it approaches 100%
            get { return new Color(1f, 0.5f, 1f); }
        }

        protected virtual int HealthPerTick
        {
            // how much health is drained per tick
            get { return 1; }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Health Catalyst");
            Tooltip.SetDefault("Hold to drain life" +
                    "\nDrain " + (int)Cap + " life to create a Beating Heart");
        }

        public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 1;
            item.rare = 2;
			item.useAnimation = 3;
			item.useTime = 3;
			item.useStyle = 4;
            item.value = Item.sellPrice(0, 1);
            item.UseSound = SoundID.Item3;
            item.autoReuse = true;
        }

		public override bool UseItem(Player player)
        {
            healthSpent += HealthPerTick; // increment stored health
            if (timer == 0) // if draining life gets a whole number percentage
            {
                timer = (int)Cap / 100;
                float progress = healthSpent / Cap;
                if (progress < 1f) Necromancy.DrainLife(player, 1, progress, ProgressColor);
            }
            else
            {
                // drain life with no visual indicator
                Necromancy.DrainLife(player, HealthPerTick, -2f);
            }
            timer--;
            item.autoReuse = true;
            use = 3;
            if (healthSpent >= Cap) // if the player has drained the maximum amount of health already
            {
                OnFull(player);
                healthSpent = 0;
                item.autoReuse = false;
                Necromancy.DrainLife(player, 0, 1f, ProgressColor);
            }
            return true;
		}

        public override void HoldItem(Player player)
        {
            // detects if the player is actively using the item or not so they can't take a break
            if (use > 0)
            {
                use -= 1;
            }
            else
            {
                OnStop(player, healthSpent);
                healthSpent = 0;
            }
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bone, 20);
            recipe.AddIngredient(mod, "BloodEssence", 10);
			recipe.AddTile(TileID.BoneWelder);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }

        protected virtual void OnFull(Player player) // when it reaches capacity
        {
            Item.NewItem(player.Center, mod.ItemType("BeatingHeart"), 1, true, 0, true);
        }

        protected virtual void OnStop(Player player, int healthSpent) // when it is interrupted before capacity
        {
        }
    }
}