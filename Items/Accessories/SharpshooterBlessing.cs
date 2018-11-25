using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Accessories
{
	public class SharpshooterBlessing : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sharpshooter's Blessing");
            Tooltip.SetDefault("25% decreased necrotic ranged damage" +
                    "\nConsecutive hits increase damage" +
                    "\nMissing a shot reduces damage");
        }

        public override void SetDefaults()
		{
			item.width = 36;
			item.height = 40;
            item.value = Item.sellPrice(0, 3);
            item.rare = 6;
			item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<NecromancyPlayer>(mod).rangedHitsAcc = true;

            // generating dusts around player - larger circle = more damage boost (see NecromancyPlayer.cs for damage calculations)
            float radius = player.GetModPlayer<NecromancyPlayer>(mod).rangedHitsNum * 5f;
            Vector2 offset = Main.rand.NextVector2CircularEdge(radius, radius);
            Dust d = Dust.QuickDust(player.Center + offset, new Color(0f, 1f, 0f));
            d.velocity = (offset * -0.1f).RotatedBy(0.6f);
            d.scale = player.GetModPlayer<NecromancyPlayer>(mod).rangedHitsNum * 0.1f;
            player.GetModPlayer<NecromancyPlayer>(mod).rangedHitsNum++;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofMight);
            recipe.AddIngredient(ItemID.SoulofSight);
            recipe.AddIngredient(ItemID.SoulofFright);
            recipe.AddIngredient(mod, "Parchment");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}