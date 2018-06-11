using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Magic
{
    public class HallowedTome : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hallowed Tome");
            Tooltip.SetDefault("Effective against vampires");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 34;
            item.crit = 4;
            item.width = 28;
            item.height = 30;
            item.useTime = 6;
            item.useAnimation = 6;
            item.useStyle = 5;
            item.knockBack = 2;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 5;
            item.noMelee = true;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("HolyLight");
            item.shootSpeed = 32f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 10;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 mouse = Main.MouseWorld;
            int numProj = Main.rand.Next(1, 3);
            for (int i = 0; i < numProj; i++)
            {
                Vector2 start = new Vector2(position.X + Main.rand.NextFloat() * 8 * 16f, position.Y - 30 * 16f);
                Vector2 vel = (mouse - start).RotatedByRandom(MathHelper.ToRadians(2));
                vel.Normalize();
                vel *= item.shootSpeed + Main.rand.NextFloat(3f);
                Projectile proj = Main.projectile[Projectile.NewProjectile(start.X, start.Y, vel.X, vel.Y, type, damage, knockBack, player.whoAmI)];
                proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
                proj.netUpdate = true;
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddIngredient(ItemID.SpellTome, 1);
            recipe.AddTile(TileID.Bookcases);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}