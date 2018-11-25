using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items
{
	public class ToughbloodPotion : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toughblood Potion");
            Tooltip.SetDefault("-2 life cost" +
                    "\n+1 life steal" +
                    "\n5 minute duration");
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
            item.value = Item.sellPrice(0, 0, 2);
            item.buffType = mod.BuffType<Buffs.Toughblood>();
            item.rare = 3;
            item.consumable = true;
        }

        public override void AddRecipes()
        {
            BloodAlchemyRecipe recipe = new BloodAlchemyRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(mod, "BloodEssence", 1);
            recipe.AddIngredient(mod, "Brimstone", 1);
            recipe.AddIngredient(ItemID.Fireblossom, 1);
            recipe.AddTile(mod, "BloodAlchemyStation");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool UseItem(Player player)
        {
            player.AddBuff(mod.BuffType<Buffs.Toughblood>(), 18000); // 5 minutes
            return true;
        }
    }
}