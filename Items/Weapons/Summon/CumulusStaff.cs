using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items.Weapons.Summon
{
	public class CumulusStaff : ModItem
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cumulus Staff");
            Tooltip.SetDefault("Summons mystical stormclouds" +
                "\nRight click to dispel");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 31;
            item.crit = 4;
            item.width = 48;
			item.height = 48;
            item.useStyle = 1;
			item.useTime = 15;
			item.useAnimation = 15;
			item.noMelee = true;
			item.knockBack = 0;
            item.autoReuse = true;
			item.value = Item.sellPrice(0, 12, 75, 0);
			item.rare = 5; 
			item.UseSound = SoundID.Item44;
			item.shoot = mod.ProjectileType("RainCloudSummon");
            item.shootSpeed = 8f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).summon = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).summonCost = 15;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            switch (Main.rand.Next(3))
            {
                case 0:
                    type = mod.ProjectileType("RainCloudSummon");
                    break;
                case 1:
                    type = mod.ProjectileType("SnowCloudSummon");
                    break;
                default:
                    type = mod.ProjectileType("LightningCloudSummon");
                    break;
            }
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.NimbusRod);
            recipe.AddIngredient(ItemID.SoulofFlight, 5);
            recipe.AddIngredient(ItemID.RainCloud, 20);
            recipe.AddIngredient(ItemID.SnowBlock, 20);
            recipe.AddIngredient(ItemID.SunplateBlock, 20);
            recipe.AddIngredient(ItemID.SoulofSight, 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}