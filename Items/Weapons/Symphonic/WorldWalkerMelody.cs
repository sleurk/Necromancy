using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items.Weapons.Symphonic
{
	public class WorldWalkerMelody : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("World-Walker's Melody");
            Tooltip.SetDefault("Shoots some wavy sound" +
                "\nEmpowers allies with bonus maximum life & mana");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 19;
            item.width = 46;
            item.height = 12;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.sellPrice(0, 2);
            item.rare = 3;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("BonevinePulse");
            item.shootSpeed = 4f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).symphonic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeCost = 5;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float mult = Main.rand.NextFloat(0.5f, 1.5f); // picks a random multiplier for the projectiles' ai1
            Vector2 vel = new Vector2(speedX, speedY);
            Projectile proj = Projectile.NewProjectileDirect(position, vel, type, damage, knockBack, player.whoAmI, 0f, mult);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            proj = Projectile.NewProjectileDirect(position, vel, type, damage, knockBack, player.whoAmI, 1f, mult);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            proj = Projectile.NewProjectileDirect(position, vel, mod.ProjectileType("MagmastarPulse"), damage, knockBack, player.whoAmI, 0f, mult);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            proj = Projectile.NewProjectileDirect(position, vel, mod.ProjectileType("MagmastarPulse"), damage, knockBack, player.whoAmI, 1f, mult);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            // ai0 = 0 or 1, changes color of the dust and projectile
            // ai1 = movement multiplier, slightly changes the trajectory of the projectile
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1f, 0f);
        }

        public override void AddRecipes()
        {
            ThoriumRecipe recipe = new ThoriumRecipe(mod);
            recipe.AddIngredient(mod, "BonevinePiccolo");
            recipe.AddIngredient(mod, "MagmastarPiccolo");
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
