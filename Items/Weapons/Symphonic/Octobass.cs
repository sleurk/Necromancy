using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Necromancy.Items.Weapons.Symphonic
{
	public class Octobass : ModItem
	{
        private int shootNum = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Octobass");
            Tooltip.SetDefault("Empowers allies with stacking damage of specific types");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 72;
            item.crit = 4;
            item.width = 48;
			item.height = 48;
            item.useStyle = 5;
			item.useTime = 8;
			item.useAnimation = 8;
            item.useStyle = 5;
            item.noMelee = true;
			item.knockBack = 5;
			item.value = Item.sellPrice(0, 12, 75, 0);
			item.rare = 7; 
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("AquaticBassBlast");
            item.shootSpeed = 8f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).symphonic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).baseLifeCost = 4;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj;

            // bass
            if (shootNum == 0)
            {
                Main.PlaySound(SoundID.Item56, player.Center);
                item.useAnimation = 16;
                item.useTime = 16;
                float numberProjectiles = 3;
                float rotation = MathHelper.ToRadians(30);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 shootVel = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                    proj = Projectile.NewProjectileDirect(position, shootVel, type, damage, knockBack, player.whoAmI);
                    proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
                }
            }

            // keytar
            if (shootNum == 1)
            {
                Main.PlaySound(SoundID.Item15, player.Center);
                item.useAnimation = 8;
                item.useTime = 8;
                proj = Projectile.NewProjectileDirect(player.Center, new Vector2(speedX, speedY), mod.ProjectileType("AquaticKeytarTune"), damage / 3, knockBack, player.whoAmI);
                proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
                proj = Projectile.NewProjectileDirect(player.Center, new Vector2(-speedX, -speedY), mod.ProjectileType("AquaticKeytarTune"), damage / 3, knockBack, player.whoAmI);
                proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            }

            // horns
            if (shootNum == 2)
            {
                Main.PlaySound(SoundID.Item43, player.Center);
                item.useAnimation = 16;
                item.useTime = 16;
                proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), mod.ProjectileType("AquaticHornDoot"), damage, knockBack, player.whoAmI);
                proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
            }

            // strings
            if (shootNum == 3)
            {
                Main.PlaySound(SoundID.Item20, player.Center);
                item.useAnimation = 16;
                item.useTime = 16;
                float numberProjectiles = 5;
                float rotation = MathHelper.ToRadians(15);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 shootVel = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                    proj = Projectile.NewProjectileDirect(position, shootVel, mod.ProjectileType("AquaticStringNote"), damage, knockBack, player.whoAmI);
                    proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
                }
            }

            // drums
            if (shootNum == 4)
            {
                Main.PlaySound(SoundID.Item10, player.Center);
                item.useAnimation = 32;
                item.useTime = 32;
                int numProjectiles = 12;
                int degrees = 360 / numProjectiles;
                for (int i = 0; i < numProjectiles; i++)
                {
                    Vector2 shootVel = new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(degrees) * i);
                    proj = Projectile.NewProjectileDirect(position, shootVel, mod.ProjectileType("AquaticDrumBeat"), damage, knockBack, player.whoAmI);
                    proj.GetGlobalProjectile<Projectiles.NecromancyGlobalProjectile>(mod).shotFrom = item;
                }
            }
            return false;
        }

        public override bool AltFunctionUse(Player player)
        {
            shootNum = (shootNum + 1) % 5;
            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10f, 0f);
        }
    }
}