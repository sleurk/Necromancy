using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Magic
{
	public class Sigil : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sigil");
            Tooltip.SetDefault("Creates a circle of damage from a small distance");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 100;
            item.width = 74;
			item.height = 74;
			item.useTime = 15;
			item.useAnimation = 15;
            item.useStyle = 5;
            item.noUseGraphic = true;
			item.value = Item.sellPrice(0, 8);
			item.rare = 8;
            item.noMelee = true;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("SigilStar");
            item.shootSpeed = 0f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeCost = 9;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                Vector2 pos1 = Main.MouseWorld - player.Center;
                Vector2 pos2 = 360f * pos1.SafeNormalize(Vector2.Zero);
                position = pos1.LengthSquared() < pos2.LengthSquared() ? pos1 : pos2; // Created at cursor, maximum distnace of 360px from player
                position += player.Center;
                Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, Main.rand.NextBool() ? -1 : 1, Main.rand.NextFloat(5f, 8f));
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            }
            return false;
        }

        // Drops from lunatic cultist boss
    }
}