using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class MidnightMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Midnight Mask");
            Tooltip.SetDefault("40% increased necrotic damage" +
                "\n-6 life cost");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 24;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 8;
            item.defense = 5;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("MidnightPlate") && legs.type == mod.ItemType("MidnightLeggings");
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<NecromancyPlayer>(mod).necroticMult += 0.4f;
            player.GetModPlayer<NecromancyPlayer>(mod).lifeCostModifier -= 6;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "+250 Max Life\nHeal for 20% of damage taken by allies";
            player.statLifeMax2 += 250;
            player.GetModPlayer<NecromancyPlayer>(mod).midnightMask = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpookyWood, 100);
            recipe.AddIngredient(ItemID.Ectoplasm, 15);
            recipe.AddIngredient(ItemID.GraniteBlock, 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
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