using Necromancy.Projectiles;
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

        // frequency multipliers to get base, third, fifth, and octave
        private readonly float[] pitchModifiers = { 1f, (float) Math.Pow(2, 5.0/12), (float)Math.Pow(2, 8.0 / 12), 2f };

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Harp");
            Tooltip.SetDefault("Empowers allies with bonus flight time");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 31;
            item.width = 48;
			item.height = 48;
            item.useStyle = 5;
			item.useTime = 15;
			item.useAnimation = 15;
            item.noUseGraphic = true;
			item.noMelee = true;
			item.knockBack = 5;
			item.value = Item.sellPrice(0, 2);
			item.rare = 4; 
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("CelestialNote");
            item.shootSpeed = 8f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).symphonic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeCost = 6;
        }

        public override void HoldItem(Player player)
        {
            // resets the note being shot if the player is not shooting
            if (holdTimer > 0)
            {
                holdTimer--;
            }
            else
            {
                shootNum = 0;
            }
        }

        // plays 4 notes (one at a time) with increasing pitch and speed, then resets
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            // plays the harp sound with a specific pitch to play specific notes
            Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/HarpPluck"), 1, pitchModifiers[shootNum]);
            holdTimer = 20; // tells the item that it is being used and not to reset the note being played
            Vector2 toMouse = Main.MouseWorld - player.Center;
            Vector2 velocity = toMouse / 15f / (4 - shootNum); // this makes all notes reach the mouse at the same time (assuming the player and mouse are still)
            speedX = velocity.X;
            speedY = velocity.Y;
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            shootNum = (shootNum + 1) % 4; // increments the note being played
            /* 
              Projectile's state was modified after it was created, and Shoot is client-only besides the creation (Projectile.NewProjectileDirect),
              so netUpdate is flagged. This tells the server and other clients to match the projectile's state with the state of the projectile
              on the projectile's owner's client.
            */
            proj.spriteDirection = -player.direction;
            proj.netUpdate = true;
            return false;
        }

        public override void AddRecipes()
        {
            ThoriumRecipe recipe = new ThoriumRecipe(mod);
            recipe.AddIngredient(mod, "CelestialBar", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}