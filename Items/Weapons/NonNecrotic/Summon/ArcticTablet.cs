using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.NonNecrotic.Summon
{
	public class ArcticTablet : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("(WIP) Arctic Tablet");
			Tooltip.SetDefault("Summons a frigid wind to fight for you");
		}

		public override void SetDefaults()
		{
			item.damage = 48;
			item.summon = true;
			item.mana = 10;
			item.width = 32;
			item.height = 32;
			item.useTime = 36;
			item.useAnimation = 36;
			item.useStyle = 1;
			item.noMelee = true;
			item.knockBack = 3;
			item.value = Item.buyPrice(1);
			item.rare = 6;
			item.UseSound = SoundID.Item44;
			item.shoot = mod.ProjectileType("ArcticWindLeader");
			item.shootSpeed = 10f;
			item.buffType = mod.BuffType("ArcticWind");	//The buff added to player after used the item
			item.buffTime = 3600;				//The duration of the buff, here is 60 seconds
		}
		
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			return player.altFunctionUse != 2;
		}
		
		public override bool UseItem(Player player)
		{
			if(player.altFunctionUse == 2)
			{
				player.MinionNPCTargetAim();
			}
			return base.UseItem(player);
		}
	}
}
