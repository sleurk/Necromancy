using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class ThunderCharge : ModProjectile
	{
        // charging projectile for Thunderbolt

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thunder Charge");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            // thorium.radiant = true
            projectile.width = 8;
			projectile.height = 8;
            projectile.tileCollide = false;
            projectile.netImportant = true;
            projectile.friendly = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 2;
            projectile.hide = true;
        }

        public override void AI()
        {
            if ((int)(projectile.ai[0]) % 3 == 1) Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/ChargeSound"), 1, 1f + (projectile.ai[0] / 60f));
            Player player = Main.player[projectile.owner];
            if (Main.myPlayer == projectile.owner)
            {
                projectile.Center = player.Center;
                projectile.velocity = (Main.MouseWorld - projectile.Center).SafeNormalize(Vector2.Zero);
                projectile.netUpdate = true;
            }
            if ((int)(projectile.ai[0]) <= 59)
            {
                Dust.QuickDust(projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.PiOver2 + projectile.ai[0] / 60f * MathHelper.Pi) * 48f, new Color(1f, 0.8f, 0f));
                Dust.QuickDust(projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.PiOver2 - projectile.ai[0] / 60f * MathHelper.Pi) * 48f, new Color(1f, 0.8f, 0f));
            }
            if ((int)(projectile.ai[0]) == 59)
            {
                for (int i = 0; i < 60; i++)
                {
                    Vector2 pos = projectile.Center + Vector2.UnitX.RotatedBy(i * MathHelper.TwoPi / 60) * 48f;
                    Dust.QuickDust(pos, new Color(1f, 0.8f, 0f)).velocity = (pos - player.Center) / 24f;
                }
            }
            projectile.ai[0] = Math.Min((int)(projectile.ai[0]) + 1, 60);
            if (player.channel && !player.dead && player.active)
            {
                projectile.timeLeft = 2;
            }
        }

        public override void Kill(int timeLeft)
        {
            if ((int)(projectile.ai[0]) == 60)
            {
                projectile.ai[0] = 120;
            }
            Main.PlaySound(SoundID.Item41, projectile.Center);
            Projectile proj = Projectile.NewProjectileDirect(projectile.Center, projectile.velocity * 4f, mod.ProjectileType("Thunderbolt"), (int)(projectile.damage * projectile.ai[0] / 30f), 
                projectile.knockBack, projectile.owner, projectile.ai[0]);
            proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = projectile.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom;
        }
    }
}