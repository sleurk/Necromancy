using Necromancy.Items.Weapons.Melee;
using Terraria;

namespace Necromancy.Items.Weapons.NonNecrotic.Melee
{
	public class SanguineLongsword : SwipeWeapon
	{
        // this is a child of SwipeWeapon.cs, so the important code is there

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sanguine Longsword");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.melee = true;
            item.magic = false;
            item.damage = 61;
            item.width = 48;
			item.height = 48;
            item.useStyle = 5;
            item.knockBack = 2f;
            item.value = Item.buyPrice(1);
			item.rare = 4;
            item.shoot = mod.ProjectileType("SanguineLongswordSwipe");
            item.shootSpeed = 64f;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = false;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).melee = false;
        }
    }
}