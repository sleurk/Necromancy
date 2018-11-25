using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items.Weapons.Ranged
{
	public class ShadowRepeater : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Repeater");
            Tooltip.SetDefault("Fires arrows extremely rapidly");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 56;
            item.width = 56;
            item.height = 26;
            item.useTime = 4;
            item.useAnimation = 4;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.sellPrice(0, 10);
            item.rare = 8;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("ShadowArrow");
            item.shootSpeed = 18f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).ranged = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeCost = 4;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal = 4;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            // 1 degree spread, very slight variance in arrow speed
            Vector2 perturbedSpeed = new Vector2(speedX, speedY);
            perturbedSpeed = perturbedSpeed.RotatedByRandom(MathHelper.ToRadians(1)) * Main.rand.NextFloat(0.95f, 1.05f);
            Projectile proj = Projectile.NewProjectileDirect(position, perturbedSpeed, type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Ectoplasm, 6);
            recipe.AddIngredient(ItemID.SpookyWood, 30);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
