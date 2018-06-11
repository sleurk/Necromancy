using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items.Weapons.Magic
{
	public class Pestilence : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pestilence");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 150;
            item.crit = 4;
            item.width = 48;
			item.height = 48;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 5;
			Item.staff[item.type] = true;
			item.noMelee = true;
			item.knockBack = 5;
			item.value = Item.sellPrice(0, 12, 75, 0);
			item.rare = 10; //changed in ModifyTooltips
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("PestilenceBall");
			item.shootSpeed = 16f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 20;
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