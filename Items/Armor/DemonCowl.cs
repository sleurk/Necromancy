using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class DemonCowl : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demon's Cowl");
            Tooltip.SetDefault("60% increased necrotic damage" +
                "\n-10 life cost");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 28;
            item.value = Item.sellPrice(0, 3);
            item.rare = 10;
            item.defense = 15;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).thoriumRarity = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<NecromancyPlayer>(mod).necroticDamage += 0.6f;
            player.GetModPlayer<NecromancyPlayer>(mod).lifeCostModifier -= 10;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("DemonGuard") && legs.type == mod.ItemType("DemonTreads");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "+500 Max Life" +
                "\nNecrotic ranged hits steal double the cost" +
                "\nNecrotic summon costs are halved";
            player.statLifeMax2 += 500;
            player.GetModPlayer<NecromancyPlayer>().demonCowl = true;
        }

        public override void AddRecipes()
        {
            Mod thorium = ModLoader.GetMod("ThoriumMod");
            if (thorium != null)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(thorium, "InfernoEssence");
                recipe.AddIngredient(thorium, "DeathEssence");
                recipe.AddIngredient(thorium, "OceanEssence");
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }

        public override void ArmorSetShadows(Player player)
        {
            if (IsArmorSet(player.armor[0], player.armor[1], player.armor[2]))
            {
                player.armorEffectDrawOutlines = true;
            }
        }
    }
}