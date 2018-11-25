using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ApocalypseHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Apocalypse Helmet");
            Tooltip.SetDefault("30% increased necrotic damage" +
                "\n-5 life cost");
        }
        
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 22;
            item.value = Item.sellPrice(0, 2);
            item.rare = 7;
			item.defense = 12;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<NecromancyPlayer>(mod).necroticDamage += 0.3f;
            player.GetModPlayer<NecromancyPlayer>(mod).lifeCostModifier -= 5;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("ApocalypseBreastplate") && legs.type == mod.ItemType("ApocalypseLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "+150 Max Life" +
                "\nAn aura surrounds you" +
                "\nPlayers inside the aura regenerate quickly" +
                "\nEnemies inside the aura take more damage";
            player.statLifeMax2 += 150;
            // uses a projectile to buff/debuff allies/enemies respectively
            Projectile.NewProjectileDirect(player.Center, Vector2.Zero, mod.ProjectileType<Projectiles.ApocalypseAura>(), 0, 0f, player.whoAmI).Center = player.Center;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddIngredient(mod, "LivingHeart", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}