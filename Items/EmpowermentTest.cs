using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items
{
	public class EmpowermentTest : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Empowerment Test");
        }

        public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;
            item.useTime = 30;
            item.useAnimation = 30;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useStyle = 5;
            item.maxStack = 1;
            item.UseSound = SoundID.Item9;
            item.rare = 8;
            item.shootSpeed = 0f;
            item.channel = true;
            item.shoot = mod.ProjectileType("EmpowermentImmortality");
		}
    }
}