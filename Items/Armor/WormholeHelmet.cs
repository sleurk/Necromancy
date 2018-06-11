using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class WormholeHelmet : ModItem
	{
        private bool defenseMode = true;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wormhole Helmet");
            Tooltip.SetDefault("40% increased necrotic damage" +
                "\n-8 life cost");
        }

        public override void SetDefaults()
		{
			item.width = 20;
			item.height = 22;
			item.value = 0;
			item.rare = 10;
			item.defense = 15;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<NecromancyPlayer>(mod).necroticMult += 0.4f;
            player.GetModPlayer<NecromancyPlayer>(mod).lifeCostModifier -= 8;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("WormholeBreastplate") && legs.type == mod.ItemType("WormholeLeggings");
		}

		public override void UpdateArmorSet(Player player)
        {
            player.statLifeMax2 += 300;
            player.GetModPlayer<NecromancyPlayer>().wormholeSet = true;
            if (defenseMode)
            {
                player.AddBuff(mod.BuffType<Buffs.WormholeDefense>(), 2);
            }
            else
            {
                player.AddBuff(mod.BuffType<Buffs.WormholeRecover>(), 2);
            }

            string button = Main.ReversedUpDownArmorSetBonuses ? "UP" : "DOWN";
            player.setBonus = "+300 Max Life" +
                "\nDouble tap " + button + " to switch between defense and offense";
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "FragmentWormhole", 10);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(TileID.LunarCraftingStation);
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

        public void SwitchMode()
        {
            defenseMode = !defenseMode;
            Main.PlaySound(SoundID.Item46);
        }
    }
}