using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.Symphonic
{
	public class KongaBongos : ModItem
    {
        public int use = 0;
        public int holdTimer = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Konga's Bongos");
            Tooltip.SetDefault("Empowers allies with stacking life regeneration");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 40;
            item.crit = 4;
            item.width = 40;
			item.height = 26;
			item.useTime = 30;
			item.useAnimation = 30;
            item.holdStyle = 3;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 5;
			item.value = Item.sellPrice(0, 0, 80, 0);
			item.rare = 7;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("KongaPulse");
			item.shootSpeed = 16f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).symphonic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 20;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            player.itemRotation = 0f;
            item.useTime = 30;
            item.useAnimation = 30;
            use++;
            holdTimer = 32;
            for (int i = 0; i < 200; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && Vector2.Distance(player.position, npc.position) < 20 * 16f)
                {
                    Projectile p = Main.projectile[Projectile.NewProjectile(npc.Center, new Vector2(), type, damage + 6 * use, knockBack + use, player.whoAmI, use == 7 ? 1 : 0)];
                    p.Center = npc.Center;
                    p.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
                }
            }
            Main.PlaySound(SoundID.Item10, player.Center);

            if (use == 6)
            {
                item.useTime = 60;
                item.useAnimation = 60;
            }
            return false;
        }

        public override void HoldItem(Player player)
        {
            if (holdTimer > 0)
            {
                holdTimer--;
            }
            else
            {
                holdTimer = 0;
                use = 0;
            }
        }

        public override void HoldStyle(Player player)
        {
            player.itemRotation = 0f;
        }

        public override void UseStyle(Player player)
        {
            player.itemRotation = 0f;
        }
    }
}