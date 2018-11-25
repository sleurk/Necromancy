using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class NecrodancerBeard : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Necrodancer's Beard");
            Tooltip.SetDefault("25% increased necrotic damage" +
                "\n-4 life cost" +
                "\n'Rise!'");
        }

        public override void SetDefaults()
		{
			item.width = 20;
			item.height = 22;
            item.value = Item.sellPrice(0, 2);
            item.rare = 6;
			item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<NecromancyPlayer>(mod).necroticDamage += 0.25f;
            player.GetModPlayer<NecromancyPlayer>(mod).lifeCostModifier -= 4;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("NecrodancerRobe") && legs.type == mod.ItemType("NecrodancerPants");
		}

		public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "+150 Max Life" +
                "\nGolden Lute's immortality charges twice as fast";
            player.GetModPlayer<NecromancyPlayer>().necrodancer = true;
            player.statLifeMax2 += 150;
        }

        public override void AddRecipes()
        {
            Mod thorium = ModLoader.GetMod("ThoriumMod");
            if (thorium != null)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(thorium, "CursedCloth", 10);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }

        public override void ArmorSetShadows(Player player)
        {
            if (IsArmorSet(player.armor[0], player.armor[1], player.armor[2]))
            {
                player.armorEffectDrawShadow = true;
            }
        }
    }
}