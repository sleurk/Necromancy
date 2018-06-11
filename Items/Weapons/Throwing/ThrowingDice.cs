using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Throwing
{
	public class ThrowingDice : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Throwing Dice");
            Tooltip.SetDefault("Throws a die that explodes with one of six effects");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 40;
            item.crit = 4;
            item.width = 18;
			item.height = 18;
			item.useTime = 30;
			item.useAnimation = 30;
            item.useStyle = 1;
            item.noUseGraphic = true;
            item.knockBack = 2;
			item.rare = 2;
            item.noMelee = true;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("ThrowingDice");
            item.shootSpeed = 16f;
            item.consumable = true;
            item.maxStack = 999;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).throwing = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).reloadCost = 50;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, Main.rand.Next(6) + 1);
            proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            proj.netUpdate = true;
            return false;
        }

        public override void AddRecipes()
        {
            Mod thorium = ModLoader.GetMod("ThoriumMod");
            if (thorium != null)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(thorium, "StrangePlating", 12);
                recipe.AddIngredient(ItemID.HallowedBar, 8);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this, 100);
                recipe.AddRecipe();
            }
        }
    }
}