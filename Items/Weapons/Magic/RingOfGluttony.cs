using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Magic
{
	public class RingOfGluttony : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ring of Gluttony");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 2;
            item.crit = 4;
            item.width = 32;
			item.height = 32;
			item.useTime = 80;
			item.useAnimation = 80;
			item.useStyle = 5;
            item.noUseGraphic = true;
			item.noMelee = true;
			item.knockBack = 5;
			item.value = Item.sellPrice(0, 0, 80, 0);
			item.rare = 1;
			item.UseSound = SoundID.Item8;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("GluttonyRing");
			item.shootSpeed = 32f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 6;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            foreach (Projectile p in Main.projectile)
            {
                if (p != null && p.active && p.type == type && p.owner == player.whoAmI)
                {
                    p.Kill();
                }
            }
            if (Main.myPlayer == player.whoAmI)
            {
                position = Main.MouseWorld;
                Projectile proj = Projectile.NewProjectileDirect(position, Vector2.Zero, type, damage, 0f, player.whoAmI);
                proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>().shotFrom = item;
                proj.Center = position;
                proj.netUpdate = true;
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SilverBar, 8);
            recipe.AddIngredient(ItemID.Topaz, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TungstenBar, 8);
            recipe.AddIngredient(ItemID.Topaz, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}