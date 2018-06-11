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
            item.crit = 4;
            item.width = 64;
			item.height = 64;
			item.useTime = 45;
			item.useAnimation = 45;
			item.useStyle = 5;
			Item.staff[item.type] = true;
			item.noMelee = true;
			item.knockBack = 5;
			item.value = Item.sellPrice(0, 3, 20, 0);
			item.rare = 8;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("MagneticPulse");
			item.shootSpeed = 3f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).magic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 50;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            player.GetModPlayer<NecromancyPlayer>(mod).magnets.Add(proj);
            return false;
        }

        public override bool AltFunctionUse(Player player)
        {
            if (!player.GetModPlayer<NecromancyPlayer>(mod).magnetsActive)
            {
                foreach (Projectile proj in player.GetModPlayer<NecromancyPlayer>(mod).magnets)
                {
                    proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).magnetActivated = true;
                    player.GetModPlayer<NecromancyPlayer>(mod).magnetsActive = true;
                }
            }
            player.GetModPlayer<NecromancyPlayer>(mod).magnets = new List<Projectile>();
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            return !player.GetModPlayer<NecromancyPlayer>(mod).magnetsActive;
        }
    }
}