using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Magic
{
	public class Rot : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rot");
            Tooltip.SetDefault("Casts a rot cloud to rain poison on your enemies");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 10;
            item.crit = 4;
            item.width = 28;
			item.height = 30;
			item.useTime = 23;
			item.useAnimation = 23;
            item.useStyle = 5;
            item.knockBack = 0;
			item.value = Item.sellPrice(0, 2, 0, 0);
			item.rare = 2;
            item.noMelee = true;
			item.UseSound = SoundID.Item20;
			item.autoReuse = false;
            item.shoot = mod.ProjectileType("RotCloudMoving");
            item.shootSpeed = 16f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 30;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            if (Main.LocalPlayer == player)
            {
                proj.ai[0] = Main.MouseWorld.X;
                proj.ai[1] = Main.MouseWorld.Y;
                proj.netUpdate = true;
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "Parchment");
            recipe.AddIngredient(ItemID.DemoniteBar, 8);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}