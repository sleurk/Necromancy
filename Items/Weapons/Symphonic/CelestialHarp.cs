using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Items.Weapons.Symphonic
{
	public class CelestialHarp : ModItem
	{
        private int shootNum = 0;

        private int holdTimer = 0;

        private float[] pitchModifiers = { 1f, (float) Math.Pow(2, 5.0/12), (float)Math.Pow(2, 8.0 / 12), 2f };

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Harp");
            Tooltip.SetDefault("Empowers allies with stacking flight time");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 31;
            item.crit = 4;
            item.width = 48;
			item.height = 48;
            item.useStyle = 5;
			item.useTime = 15;
			item.useAnimation = 15;
            item.noUseGraphic = true;
			item.noMelee = true;
			item.knockBack = 5;
			item.value = Item.sellPrice(0, 12, 75, 0);
			item.rare = 4; 
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("CelestialNote");
            item.shootSpeed = 8f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).symphonic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 4;
        }

        public override void HoldItem(Player player)
        {
            if (holdTimer > 0)
            {
                holdTimer--;
            }
            else
            {
                shootNum = 0;
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.whoAmI == Main.myPlayer) // test
            {
                Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/HarpPluck"), 1, pitchModifiers[shootNum]);
                holdTimer = 20;
                Vector2 toMouse = Main.MouseWorld - player.Center;
                Vector2 velocity = toMouse / 15f / (4 - shootNum);
                speedX = velocity.X;
                speedY = velocity.Y;
                Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
                proj.netUpdate = true;
                proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
                proj.spriteDirection = -player.direction;
                shootNum++;
                shootNum = shootNum % 4;
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CelestialBar", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}