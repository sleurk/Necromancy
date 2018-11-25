using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items.Weapons.Summon
{
	public class CrimsonFamiliar : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimson Familiar");
            Tooltip.SetDefault("Summons a blood zone that follows your screen" +
                "\nRight click to dispel");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 43;
            item.width = 48;
			item.height = 48;
            item.useStyle = 1;
			item.useTime = 15;
			item.useAnimation = 15;
			item.noMelee = true;
			item.knockBack = 0f;
            item.autoReuse = true;
            item.value = Item.buyPrice(1);
            item.rare = 4;
			item.UseSound = SoundID.Item44;
			item.shoot = mod.ProjectileType("CrimsonFamiliar");
            item.shootSpeed = 0f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).summon = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).summonCost = 25;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 pos = Main.MouseWorld;
            Vector2 relative = pos - player.Center;
            // creates the projectile at cursor, and tells the projectile its relative position to the player with ai0 and ai1
            Projectile proj = Projectile.NewProjectileDirect(pos, Vector2.Zero, type, damage, knockBack, player.whoAmI, relative.X, relative.Y);
            // ai0 = projectile's x coordinate relative to the player's
            // ai1 = projectile's y coordinate relative to the player's
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }
    }
}