using Necromancy.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Items.Weapons.NonNecrotic.Radiant
{
    public class MeteorEye : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Meteor Eye");
            Tooltip.SetDefault("Summon large meteors from the sky");
        }

        public override void SetDefaults()
        {
            item.magic = true;
            // thorium.radiant = true; this is not supported yet because I have not yet spoken to thorium devs about setting up weak references
            item.damage = 72;
            item.width = 64;
            item.height = 68;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.knockBack = 2;
            item.value = Item.buyPrice(1);
            item.rare = 6;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("MeteorEye");
            item.mana = 25;
            item.shootSpeed = 8f;
            item.prefix = 0;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            // creates the projectile above the screen towards the cursor

            Vector2 mouse = Main.MouseWorld;
            Vector2 start = new Vector2(position.X + Main.rand.NextFloat() * 128, position.Y - 480f);
            Vector2 vel = (mouse - start).RotatedByRandom(MathHelper.ToRadians(1));
            
            vel = vel.SafeNormalize(Vector2.Zero) * item.shootSpeed;
            Projectile proj = Main.projectile[Projectile.NewProjectile(start.X, start.Y, vel.X, vel.Y, type, damage, knockBack, player.whoAmI)];
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shotFrom = item;
            return false;
        }
    }
}