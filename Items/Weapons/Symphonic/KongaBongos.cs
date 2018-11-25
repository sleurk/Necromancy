using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Symphonic
{
	public class KongaBongos : ModItem
    {
        public int use = 0;
        public int holdTimer = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Konga's Bongos");
            Tooltip.SetDefault("Empowers allies with bonus life regeneration");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 40;
            item.width = 44;
			item.height = 26;
			item.useTime = 30;
			item.useAnimation = 30;
            item.holdStyle = 3;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 0f;
			item.value = Item.sellPrice(0, 5);
			item.rare = 7;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("KongaPulse");
			item.shootSpeed = 16f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).symphonic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeCost = 10;
        }

        // plays 7 notes (1 at a time) with increasing power, then pauses after the seventh
        // each note creates an invisible projectile
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            player.itemRotation = 0f;
            item.useTime = 30;
            item.useAnimation = 30;
            use++; // use = (which note it is playing) - 1
            holdTimer = 32;
            for (int i = 0; i < 200; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && Vector2.Distance(player.position, npc.position) < 20 * 16f)
                {
                    Projectile p = Main.projectile[Projectile.NewProjectile(npc.Center, new Vector2(), type, damage + 6 * use, 0f, player.whoAmI, use == 7 ? 1 : 0)];
                    p.Center = npc.Center;
                    p.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
                }
            }
            Main.PlaySound(SoundID.Item10, player.Center);

            if (use == 6) // seventh note
            {
                // reduces the fire rate, creates a one second delay before the next shot, which also resets the note in HoldItem because of the delay
                item.useTime = 60;
                item.useAnimation = 60;
            }
            return false;
        }

        public override void HoldItem(Player player)
        {
            // resets which note is being played if the weapon is not being used
            if (holdTimer > 0)
            {
                holdTimer--;
            }
            else
            {
                holdTimer = 0;
                use = 0;
            }
        }

        public override void HoldStyle(Player player)
        {
            player.itemRotation = 0f;
        }

        public override void UseStyle(Player player)
        {
            player.itemRotation = 0f;
        }

        public override void AddRecipes()
        {
            ThoriumRecipe recipe = new ThoriumRecipe(mod);
            recipe.AddIngredient(mod, "LivingHeart");
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}