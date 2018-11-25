using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.NonNecrotic.Summon
{
	public class Firewall : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("(WIP) Firewall");
            Tooltip.SetDefault("Creates a wall of fire");
        }

        public override void SetDefaults()
        {
            item.damage = 69;
            item.width = 30;
			item.height = 50;
			item.useTime = 30;
			item.useAnimation = 30;
            item.useStyle = 5;
            item.noUseGraphic = true;
			item.value = Item.buyPrice(1);
			item.rare = 6;
            item.channel = true;
            item.noMelee = true;
			item.UseSound = SoundID.Item88;
            item.shoot = mod.ProjectileType("FirewallPrime");
            item.shootSpeed = 0f;
            item.prefix = 0;
            item.buffType = mod.BuffType("Firewall"); //The buff added to player after used the item
            item.buffTime = 3600;				//The duration of the buff, here is 60 seconds
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2) return false;
            if (Main.myPlayer == player.whoAmI)
            {
                position = Main.MouseWorld;
                Projectile proj = Projectile.NewProjectileDirect(position, Vector2.Zero, type, damage, knockBack, player.whoAmI);
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;

                proj.netUpdate = true;
            }
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