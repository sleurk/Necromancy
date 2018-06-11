using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class HallowedSkull : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hallowed Skull");
            Tooltip.SetDefault("20% increased necrotic damage" +
                "\n-3 life cost");
        }

        public override void SetDefaults()
		{
			item.width = 20;
			item.height = 22;
			item.value = 0;
			item.rare = 5;
			item.defense = 5;
            item.value = Item.sellPrice(0, 5, 0, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<NecromancyPlayer>().necroticMult += 0.2f;
            player.GetModPlayer<NecromancyPlayer>().lifeCostModifier -= 3;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemID.HallowedPlateMail && legs.type == ItemID.HallowedGreaves;
        }

		public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "+150 Max Life" +
                "\nImmune to most damaging debuffs";
            player.statLifeMax2 += 150;
            player.GetModPlayer<NecromancyPlayer>().hallowedSkull = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void ArmorSetShadows(Player player)
        {
            if (IsArmorSet(player.armor[0], player.armor[1], player.armor[2]))
            {
                player.armorEffectDrawShadow = true;
                player.armorEffectDrawOutlines = true;
            }
        }
    }
}