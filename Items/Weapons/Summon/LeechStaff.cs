using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items.Weapons.Summon
{
	public class LeechStaff : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("(NYI) Leech Staff");
            Tooltip.SetDefault("Lifesteal for 10% of damage dealt" +
                "\nThe leech can climb walls with its mouth?" +
                "\nThis summon is very broken, please don't try to use it" +
                "\nRight click to dispel");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 11;
            item.width = 48;
			item.height = 48;
            item.useStyle = 1;
			item.useTime = 15;
			item.useAnimation = 15;
			item.noMelee = true;
			item.knockBack = 0f;
            item.autoReuse = true;
            item.value = Item.sellPrice(0, 1);
            item.rare = 3; 
			item.UseSound = SoundID.Item44;
			item.shoot = mod.ProjectileType("Leech");
            item.shootSpeed = 0f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).summon = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).summonCost = 10;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            Mod thorium = ModLoader.GetMod("ThoriumMod");
            if (thorium != null)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(thorium, "DepthScale", 8);
                recipe.AddIngredient(thorium, "AquaiteBar", 10);
                recipe.AddTile(TileID.Anvils);
                recipe.SetResult(this);
                // recipe.AddRecipe(); NYI
            }
        }
    }
}