using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items
{
	public class DestabilizationPotion : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Destabilization Potion");
            Tooltip.SetDefault("Chance to teleport on hit" +
                "\n6 minute duration");
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
			item.rare = 4;
            item.consumable = true;
        }

        public override void AddRecipes()
        {
            BloodAlchemyRecipe recipe = new BloodAlchemyRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(mod, "ShadowBlood", 1);
            recipe.AddIngredient(ItemID.CrystalShard, 1);
            recipe.AddTile(mod, "BloodAlchemyStation");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool UseItem(Player player)
        {
            Necromancy.DrainLife(player, 50);
            player.AddBuff(mod.BuffType<Buffs.Destabilized>(), 21600); // 6 minutes
            return true;
        }
    }
}