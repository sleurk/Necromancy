using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items.Weapons.Summon
{
	public class CelestialStaff : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Staff");
            Tooltip.SetDefault("Summons a celestial pixie" +
                "\nRight click to dispel");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 31;
            item.width = 48;
			item.height = 48;
            item.useStyle = 1;
			item.useTime = 15;
			item.useAnimation = 15;
			item.noMelee = true;
			item.knockBack = 5;
            item.autoReuse = true;
            item.value = Item.sellPrice(0, 2);
            item.rare = 4; 
			item.UseSound = SoundID.Item44;
			item.shoot = mod.ProjectileType("CelestialPixie");
            item.shootSpeed = 8f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).summon = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).summonCost = 20;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "CelestialBar", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}