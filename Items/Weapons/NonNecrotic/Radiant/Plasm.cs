using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.NonNecrotic.Radiant
{
	public class Plasm : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plasm");
            Tooltip.SetDefault("Creates beams of light that lock on to multiple nearby targets");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            // thorium.radiant = true; this is not supported yet because I have not yet spoken to thorium devs about setting up weak references
            item.damage = 36;
            item.width = 36;
			item.height = 44;
            item.useTime = 30;
            item.useAnimation = 30;
			item.useStyle = 5;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.knockBack = 5;
            item.value = Item.buyPrice(1);
            item.rare = 4;
            item.shoot = mod.ProjectileType("PlasmStart");
            item.mana = 24;
            item.shootSpeed = 1f;
            item.prefix = 0;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(SoundID.Item42, player.Center);
            MakeProjectiles(player, 1f, type, damage, knockBack);
            MakeProjectiles(player, -1f, type, damage, knockBack);
            return false;
        }

        private void MakeProjectiles(Player player, float speedX, int type, int damage, float knockBack)
        {
            // creates many projectiles in a line
            float dist = 0f;
            Projectile proj;
            proj = Projectile.NewProjectileDirect(player.Center + Vector2.UnitX * dist, new Vector2(speedX, 0f), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            proj.spriteDirection = Math.Sign(speedX);
            for (int i = 0; i < 30; i++)
            {
                dist += speedX * 48f;
                proj = Projectile.NewProjectileDirect(player.Center + Vector2.UnitX * dist, new Vector2(speedX, 0f), mod.ProjectileType("PlasmBlast" + (i % 4 + 1)), damage, knockBack, player.whoAmI, dist);
                // ai0 = distance factor, for visual effects
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = item;
                /* 
                  Projectile's state was modified after it was created, and Shoot is client-only besides the creation (Projectile.NewProjectileDirect),
                  so netUpdate is flagged. This tells the server and other clients to match the projectile's state with the state of the projectile
                  on the projectile's owner's client.
                 */
                proj.spriteDirection = Math.Sign(speedX);
                proj.alpha = i * 10;
                proj.netUpdate = true;
            }
        }
    }
}