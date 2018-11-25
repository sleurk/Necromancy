using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class BoneplateHood : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boneplate Hood");
            Tooltip.SetDefault("10% increased necrotic damage" +
                "\n-1 life cost");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.value = Item.sellPrice(0, 1);
            item.rare = 2;
            item.defense = 4;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<NecromancyPlayer>(mod).necroticDamage += 0.1f;
            player.GetModPlayer<NecromancyPlayer>(mod).lifeCostModifier -= 1;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("BoneplateChestpiece") && legs.type == mod.ItemType("BoneplateGreaves");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "+50 Max Life" +
                "\nNecrotic critical hits stun enemies";
            player.statLifeMax2 += 50;
            player.GetModPlayer<NecromancyPlayer>().boneplate = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bone, 20);
            recipe.AddIngredient(ItemID.DemoniteBar, 15);
            recipe.AddIngredient(mod, "BeatingHeart");
            recipe.AddTile(TileID.BoneWelder);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bone, 20);
            recipe.AddIngredient(ItemID.CrimtaneBar, 15);
            recipe.AddIngredient(mod, "BeatingHeart");
            recipe.AddTile(TileID.BoneWelder);
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