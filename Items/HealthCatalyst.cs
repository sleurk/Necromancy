using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items
{
	public class HealthCatalyst : ModItem
	{
        private int healthSpent = 0;
        private int use = 0; // to keep track of if the button is held down or not

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Health Catalyst");
            Tooltip.SetDefault("Hold to drain life" +
                    "\nDrain 400 life to create a Beating Heart");
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
            item.value = Item.sellPrice(0, 0, 50, 0);
            item.UseSound = SoundID.Item3;
            item.autoReuse = true;
        }

		public override bool UseItem(Player player)
        {
            item.autoReuse = true;
            use = 3;
            healthSpent += 1;
            Necromancy.DrainLife(player, 1);
            if (healthSpent >= 400)
            {
                Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("BeatingHeart"), 1);
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
            recipe.AddIngredient(ItemID.Bone, 20);
            recipe.AddIngredient(null, "BloodEssence", 10);
			recipe.AddTile(TileID.BoneWelder);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
	}
}