using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items.Weapons.NonNecrotic.Summon
{
	public class GreenSwarm : ModItem
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("(WIP) Green Swarm");
            Tooltip.SetDefault("Summons a swarm of... something" +
                "\nRight click to dispel");
        }

        public override void SetDefaults()
        {
            item.summon = true;
            item.damage = 52;
            item.width = 64;
			item.height = 64;
            item.useStyle = 1;
			item.useTime = 30;
			item.useAnimation = 30;
			item.noMelee = true;
			item.knockBack = 0f;
            item.value = Item.buyPrice(1);
            item.rare = 7; 
			item.UseSound = SoundID.Item44;
			item.shoot = mod.ProjectileType("GreenSwarmLeader");
            item.shootSpeed = 16f;
            item.prefix = 0;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2) return false;
            player.AddBuff(mod.BuffType("GreenSwarm"), 3600);
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim();
            }
            return base.UseItem(player);
        }
    }
}