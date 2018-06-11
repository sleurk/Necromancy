using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items
{
	public class DarkPowerPotion : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Power Potion");
            Tooltip.SetDefault("20% increased necrotic damage" +
                    "\n+3 life cost" +
                    "\n3 minute duration");
        }

        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.MagicPowerPotion);
            item.width = 20;
			item.height = 30;
			item.maxStack = 30;
            item.useStyle = refItem.useStyle;
            item.useTime = refItem.useTime;
            item.useAnimation = refItem.useAnimation;
            item.UseSound = refItem.UseSound;
			item.value = Item.sellPrice(0, 0, 2, 0);
			item.rare = 2;
            item.potion = true;
            item.consumable = true;
        }

        public override void AddRecipes()
        {
            BloodAlchemyRecipe recipe = new BloodAlchemyRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(mod, "BloodEssence", 1);
            recipe.AddIngredient(mod, "BeatingHeart", 1);
            recipe.AddIngredient(ItemID.Deathweed, 1);
            recipe.AddTile(mod, "BloodAlchemyStation");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool UseItem(Player player)
        {
            player.AddBuff(mod.BuffType<Buffs.DarkPower>(), 10800); // 3 minutes
            return true;
        }
    }
}