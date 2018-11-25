using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.NonNecrotic.Symphonic
{
	public class ChillingHarmonica : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chilling Harmonica");
            Tooltip.SetDefault("Empowers allies with bonus damage to chilled enemies");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            // thorium.symphonic = true this is not supported yet because I have not yet spoken to thorium devs about setting up weak references
            item.damage = 45;
            item.width = 50;
			item.height = 28;
			item.useTime = 16;
			item.useAnimation = 16;
			item.useStyle = 5;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.knockBack = 5;
            item.value = Item.buyPrice(1);
            item.rare = 6;
			item.UseSound = SoundID.Item43;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("ChillingBreath");
			item.shootSpeed = 8f;
            item.prefix = 0;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            List<Projectile> projectiles = new List<Projectile>();
            foreach (Projectile proj in Main.projectile)
            {
                if (proj != null && proj.active && proj.type == item.shoot && proj.owner == player.whoAmI)
                {
                    projectiles.Add(proj);
                }
            }
            projectiles.Sort(new ProjectileAgeComparer());
            // maximum of 45 projectiles, new ones replace old ones
            for (int i = projectiles.Count - 1; i >= 45; i--)
            {
                projectiles[i].Kill();
            }

            // creates 5 parallel projectiles evenly spread
            for (int i = -2; i <= 2; i++)
            {
                Vector2 vel = new Vector2(speedX, speedY);
                Projectile proj = Projectile.NewProjectileDirect(position + vel.RotatedBy(MathHelper.PiOver2) * i, vel, type, damage, knockBack, player.whoAmI);
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            }

            return false;
        }
    }
}