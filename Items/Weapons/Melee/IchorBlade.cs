using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Melee
{
    public class IchorBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ichor Blade");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 31;
            item.width = 40;
            item.height = 40;
            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = Item.sellPrice(0, 2);
            item.autoReuse = true;
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.prefix = 0;
            item.shoot = mod.ProjectileType("IchorBlade");
            item.shootSpeed = 0f;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).melee = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).ichor = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal = 3;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            // swings sword and also shoots a slow projectile
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, Main.rand.NextFloat(0f, MathHelper.TwoPi), player.direction);
            // ai0 = starting rotation
            // ai1 = rotation direction
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            proj.Center = player.Center;
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "IchorBar", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(3) == 0)
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 246);
            }
        }
    }
}
