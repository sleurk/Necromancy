using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class BloodclothHood : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloodcloth Hood");
            Tooltip.SetDefault("5% increased necrotic damage");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.value = Item.sellPrice(0, 0, 20);
            item.rare = 1;
            item.defense = 2;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<NecromancyPlayer>(mod).necroticDamage += .05f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("BloodclothRobe") && legs.type == mod.ItemType("BloodclothPants");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "+20 Max Life" +
                "\nNecrotic damage wounds enemies";
            player.statLifeMax2 += 20;
            player.GetModPlayer<NecromancyPlayer>().bloodcloth = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "BloodEssence", 5);
            recipe.AddIngredient(ItemID.Silk, 4);
            recipe.AddTile(TileID.Loom);
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