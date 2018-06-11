using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Melee
{
	public class Channeler : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Channeler");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 63;
            item.crit = 4;
            item.width = 64;
			item.height = 64;
			item.useTime = 10;
			item.useAnimation = 12;
			item.useStyle = 5;
			item.knockBack = 6;
            item.channel = true;
            item.noUseGraphic = true;
			item.useTurn = false;﻿
            item.noMelee = true;
			item.rare = 7;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Channeler");
			item.shootSpeed = 2.4f;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).melee = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal = 5;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "LivingHeart");
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool CanUseItem(Player player)
		{
            return player.ownedProjectileCounts[item.shoot] < 1;
		}
    }
}
