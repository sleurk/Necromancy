using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Symphonic
{
	public class GoldenLute : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Golden Lute");
            Tooltip.SetDefault("Empowers allies with a charge-based extra life");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 35;
            item.width = 60;
			item.height = 56;
			item.useTime = 10;
            item.useAnimation = 10;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 0f;
			item.value = Item.sellPrice(0, 20);
			item.rare = 6;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("LuteBlast");
			item.shootSpeed = 16f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).symphonic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeCost = 7;
        }

        // shoots faster, farther away, and with higher pitch and damage the faster the player is moving
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float speedMult = MathHelper.Min(1 + player.velocity.Length() / 15f, 2f); // speed multiplier from player movement
            item.useTime = (int)(10 / speedMult); // multiplier increases fire rate
            item.useAnimation = (int)(10 / speedMult);
            item.damage = (int)(35 * speedMult); // multiplier increases damage
            Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/HarpPluck"), 1, speedMult); // multiplier increases pitch
            Vector2 pos = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero) * 200f * (speedMult - 0.8f); // multiplier increases distance from player
            pos += player.Center;
            Projectile proj = Projectile.NewProjectileDirect(pos, Vector2.Zero, type, damage, knockBack, player.whoAmI, speedMult);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override void AddRecipes()
        {
            // temporary recipe, weapon will be purchased from upcoming Shopkeeper NPC when wearing necrodancer armor
            Mod thorium = ModLoader.GetMod("ThoriumMod");
            if (thorium != null)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(thorium, "CursedCloth", 10);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10f, 0f);
        }
    }
}