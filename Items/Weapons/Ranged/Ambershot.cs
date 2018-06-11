using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items.Weapons.Ranged
{
	public class Ambershot : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ambershot");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.ranged = true;
            item.damage = 12;
            item.crit = 4;
            item.width = 46;
            item.height = 34;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = Item.sellPrice(0, 22, 0, 0);
            item.rare = 2;
            item.UseSound = SoundID.Item41;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("AmberBullet");
            item.shootSpeed = 60f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).ranged = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 4;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeSteal = 4;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(3));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            Vector2 muzzleOffset = new Vector2(0, -8);
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FossilOre, 20);
            recipe.AddIngredient(ItemID.Amber, 6);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7, 0);
        }
    }
}
