using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items.Weapons.Summon
{
	public class Famine : ModItem
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Famine");
            Tooltip.SetDefault("Summons a cloud of darkness that envelops enemies");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 73;
            item.crit = 4;
            item.width = 48;
			item.height = 48;
            item.useStyle = 1;
			item.useTime = 15;
			item.useAnimation = 15;
			item.noMelee = true;
			item.knockBack = 5;
            item.autoReuse = true;
			item.value = Item.sellPrice(0, 12, 75, 0);
			item.rare = 10; 
			item.UseSound = SoundID.Item44;
			item.shoot = mod.ProjectileType("DarkCloud");
            item.shootSpeed = 8f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).summon = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).summonCost = 20;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).thoriumRarity = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
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
    }
}