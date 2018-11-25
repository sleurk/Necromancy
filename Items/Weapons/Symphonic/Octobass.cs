using Necromancy.Projectiles;
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
            Tooltip.SetDefault("Empowers allies with bonus damage of specific types");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 72;
            item.width = 48;
			item.height = 48;
            item.useStyle = 5;
			item.useTime = 8;
			item.useAnimation = 8;
            item.useStyle = 5;
            item.noMelee = true;
			item.knockBack = 5;
			item.value = Item.sellPrice(0, 20);
			item.rare = 7; 
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("AquaticBassBlast");
            item.shootSpeed = 8f;
            item.prefix = 0;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).necrotic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).symphonic = true;
            item.GetGlobalItem<NecromancyGlobalItem>(mod).lifeCost = 14;
        }

        // this weapon cycles through 8 different modes and shoots 8 different types of projectiles all in different ways
        // shootNum = which type of projectile it shoots
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj;
            Main.PlaySound(SoundID.Item56, player.Center);

            // melee damage buff projectile
            if (shootNum == 0)
            {
                // shoots 12 projectiles in a ring
                item.useAnimation = 32;
                item.useTime = 32;
                int numProjectiles = 12;
                int degrees = 360 / numProjectiles;
                for (int i = 0; i < numProjectiles; i++)
                {
                    Vector2 shootVel = new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(degrees) * i);
                    proj = Projectile.NewProjectileDirect(position, shootVel, mod.ProjectileType("AquaticDrumBeat"), damage, knockBack, player.whoAmI);
                    proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
                }
            }

            // ranged damage buff projectile
            if (shootNum == 1)
            {
                item.useAnimation = 16;
                item.useTime = 16;
                proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), mod.ProjectileType("AquaticHornDoot"), damage, knockBack, player.whoAmI);
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            }

            // magic damage buff projectile
            if (shootNum == 2)
            {
                // shoots in front of and behind player
                Main.PlaySound(SoundID.Item15, player.Center);
                item.useAnimation = 8;
                item.useTime = 8;
                proj = Projectile.NewProjectileDirect(player.Center, new Vector2(speedX, speedY), mod.ProjectileType("AquaticKeytarTune"), damage / 3, knockBack, player.whoAmI);
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
                proj = Projectile.NewProjectileDirect(player.Center, new Vector2(-speedX, -speedY), mod.ProjectileType("AquaticKeytarTune"), damage / 3, knockBack, player.whoAmI);
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            }

            // summon damage buff projectile
            if (shootNum == 3)
            {
                // shoots 5 projectiles with 15 degree even spacing
                item.useAnimation = 16;
                item.useTime = 16;
                float numberProjectiles = 5;
                float rotation = MathHelper.ToRadians(15);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 shootVel = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                    proj = Projectile.NewProjectileDirect(position, shootVel, mod.ProjectileType("AquaticStringNote"), damage, knockBack, player.whoAmI);
                    proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
                }
            }

            // thrown damage buff projectile
            if (shootNum == 4)
            {
                // shoots projectiles with 3 degree spread
                item.useAnimation = 4;
                item.useTime = 4;
                Vector2 shootVel = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(3));
                proj = Projectile.NewProjectileDirect(position, shootVel.SafeNormalize(Vector2.UnitX), mod.ProjectileType("AquaticBassPulse"), damage / 2, knockBack, player.whoAmI, Main.rand.NextFloat(-1f, 1f));
                // ai0 = gravity of projectile
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            }

            // radiant damage buff projectile
            if (shootNum == 5)
            {
                // creates the projectile diagonally from the mouse
                item.useAnimation = 8;
                item.useTime = 8;
                Vector2 vel = new Vector2(Main.rand.NextBool() ? -1f : 1f, Main.rand.NextBool() ? -1f : 1f).SafeNormalize(Vector2.Zero);
                proj = Projectile.NewProjectileDirect(Main.MouseWorld - vel * 100f, vel, mod.ProjectileType("AquaticBassSlash"), damage, knockBack, player.whoAmI);
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            }

            // symphonic damage buff projectile
            if (shootNum == 6)
            {
                // creates a projectile a little above where it would normally spawn for the wave to work
                item.useAnimation = 8;
                item.useTime = 8;
                proj = Projectile.NewProjectileDirect(position + new Vector2(0f, -40f), new Vector2(speedX, speedY), mod.ProjectileType("AquaticBassWave"), damage, knockBack, player.whoAmI);
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            }

            // necrotic damage buff projectile
            if (shootNum == 7)
            {
                item.useAnimation = 16;
                item.useTime = 16;
                float numberProjectiles = 3;
                float rotation = MathHelper.ToRadians(15);
                // shoots 3 projectiles with 15 degree spacing
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 shootVel = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                    proj = Projectile.NewProjectileDirect(position, shootVel, type, damage, knockBack, player.whoAmI);
                    proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
                }
            }
            return false;
        }

        public override bool AltFunctionUse(Player player)
        {
            // right click switches shooting mode
            shootNum = (shootNum + 1) % 8;
            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10f, 0f);
        }
    }
}