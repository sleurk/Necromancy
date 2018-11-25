using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class IceWhip : ModProjectile
	{
        // whip projectile, moves fast in a straight line
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Whip");
        }

        public override void SetDefaults()
        {
            projectile.melee = true;
            projectile.width = 4;
			projectile.height = 4;
            projectile.friendly = true;
            projectile.penetrate = 1;
			projectile.timeLeft = 120;
            projectile.hide = true;
            projectile.extraUpdates = 20;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).ice = true;

        }

		public override void AI()
		{
            for (int i = 0; i < 4; i++)
            { 
                Dust d = Dust.QuickDust(projectile.Center + projectile.velocity * i / 4f, new Color(0f, 0.6f, 0.7f));
                d.noGravity = true;
                d.scale = Main.rand.NextFloat();
                d.velocity *= 0.2f;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 300);
            target.AddBuff(BuffID.Frozen, 60);
        }
    }
}