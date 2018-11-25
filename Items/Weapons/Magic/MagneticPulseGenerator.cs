using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items.Weapons.Magic
{
	public class MagneticPulseGenerator : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magnetic Pulse Generator");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 40;
            item.width = 64;
			item.height = 64;
			item.useTime = 45;
			item.useAnimation = 45;
			item.useStyle = 5;
			Item.staff[item.type] = true;
			item.noMelee = true;
			item.knockBack = 5;
			item.value = Item.sellPrice(0, 6);
			item.rare = 8;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("MagneticPulse");
			item.shootSpeed = 3f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeCost = 50;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override bool AltFunctionUse(Player player)
        {
            foreach (Projectile proj in Main.projectile)
            {
                // right click 'activates' all MagneticPulse projectiles, creating MagneticPulseActive projectiles (see MagneticPulse.cs)
                if (proj != null && proj.active && proj.owner == player.whoAmI)
                {
                    proj.ai[0] = 1f;
                    proj.netUpdate = true;
                }
            }
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            // cannot shoot more MagneticPulse projectiles while there are some active in the world
            return player.ownedProjectileCounts[mod.ProjectileType("MagneticPulseActive")] == 0;
        }
    }
}