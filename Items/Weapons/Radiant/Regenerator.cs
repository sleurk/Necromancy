using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items.Weapons.Radiant
{
	public class Regenerator : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Regenerator");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 35;
            item.width = 48;
            item.height = 26;
            item.useTime = 3;
            item.useAnimation = 3;
            item.useStyle = 5;
            item.noMelee = true;
            item.autoReuse = true;
            item.knockBack = 8;
            item.value = Item.sellPrice(0, 5);
            item.rare = 7;
            item.UseSound = SoundID.Item13;
            item.shoot = mod.ProjectileType("RegeneratorBeam");
            item.shootSpeed = 8f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).radiant = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeCost = 6;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).healPower = 3;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float rotate = Main.rand.NextFloat(-1f, 1f);
            Vector2 shoot = new Vector2(speedX, speedY);
            position += shoot * 3f; // move starting position forward
            shoot = shoot.RotatedByRandom(MathHelper.ToRadians(20f * rotate)); // random rotation, higher angles are rarer
            Projectile proj = Projectile.NewProjectileDirect(position, shoot, type, damage, knockBack, player.whoAmI, -rotate);
            // ai1: opposite of initial rotation, for calculating trajectory
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            ThoriumRecipe recipe = new ThoriumRecipe(mod);
            recipe.AddIngredient(mod, "LivingHeart");
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, -5);
        }
    }
}
