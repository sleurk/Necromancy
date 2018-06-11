using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items
{
	public class GreaterHealthCatalyst : ModItem
	{
        private int healthSpent = 0;
        private int use = 0; // to keep track of if the button is held down or not

        public override void SetStaticDefaults()
        {
            if (healthSpent > 0)
            {
                DisplayName.SetDefault("Greater Health Catalyst: " + healthSpent + " / 500");
            }
            else
            {
                DisplayName.SetDefault("Greater Health Catalyst");
            }
            Tooltip.SetDefault("Hold to drain life" +
                    "\nDrain 500 life to create a Living Heart");
        }

        public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 1;
            item.rare = 7;
			item.useAnimation = 3;
			item.useTime = 3;
			item.useStyle = 4;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.UseSound = SoundID.Item3;
            item.autoReuse = true;
        }

		public override bool UseItem(Player player)
        {
            item.autoReuse = true;
            use = 3;
            healthSpent += 1;
            Necromancy.DrainLife(player, 1);
            if (healthSpent >= 500)
            {
                Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("LivingHeart"), 1);
                healthSpent = 0;
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
            else
            {
                healthSpent = 0;
            }
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
	}
}