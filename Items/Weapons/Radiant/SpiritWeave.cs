using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Radiant
{
    public class SpiritWeave : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirit Weave");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 43;
            item.crit = 4;
            item.width = 40;
            item.height = 40;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.knockBack = 5;
            item.value = Item.sellPrice(0, 0, 80, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("EnergyRainbow");
            item.shootSpeed = 3f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).radiant = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 4;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).healPower = 4;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "WebrootWand");
            recipe.AddIngredient(mod, "HeartbloomWand");
            recipe.AddIngredient(mod, "FrostthornWand");
            recipe.AddIngredient(mod, "DarkweedWand");
            recipe.AddIngredient(mod, "FossilleafWand");
            recipe.AddIngredient(mod, "PearlkelpWand");
            recipe.AddIngredient(mod, "GrowthglowWand");
            recipe.AddIngredient(mod, "AshblossomWand");
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "WebrootWand");
            recipe.AddIngredient(mod, "HeartbloomWand");
            recipe.AddIngredient(mod, "FrostthornWand");
            recipe.AddIngredient(mod, "BloodweedWand");
            recipe.AddIngredient(mod, "FossilleafWand");
            recipe.AddIngredient(mod, "PearlkelpWand");
            recipe.AddIngredient(mod, "GrowthglowWand");
            recipe.AddIngredient(mod, "AshblossomWand");
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}