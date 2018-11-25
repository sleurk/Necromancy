using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Necromancy.Items.Weapons.NonNecrotic.Summon
{
	public class StormCaller : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("(WIP) Storm Caller");
            Tooltip.SetDefault("Summons a thunderstorm to strike at up to 5 enemies below it" +
                "\nRight click to dispel");
        }

        public override void SetDefaults()
        {
            item.summon = true;
            item.sentry = true;
            item.damage = 71;
            item.width = 40;
			item.height = 48;
            item.useStyle = 1;
			item.useTime = 30;
			item.useAnimation = 30;
			item.noMelee = true;
			item.knockBack = 0f;
            item.value = Item.buyPrice(1);
            item.rare = 8;
			item.UseSound = SoundID.Item44;
			item.shoot = mod.ProjectileType("StormCloud");
            item.shootSpeed = 0f;
            item.prefix = 0;
            item.buffType = mod.BuffType("Thunderstorm"); //The buff added to player after used the item
            item.buffTime = 3600;				//The duration of the buff, here is 60 seconds
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2) return false;
            Vector2 pos = Main.MouseWorld;
            Projectile proj = Projectile.NewProjectileDirect(pos, Vector2.Zero, type, damage, knockBack, player.whoAmI);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            proj.netUpdate = true;
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