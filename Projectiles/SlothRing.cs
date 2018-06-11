using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace Necromancy.Projectiles
{
	public class SlothRing : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sloth Ring");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 100;
			projectile.height = 100;
			projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.hide = true;
			projectile.penetrate = 15;
			projectile.timeLeft = 600;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.velocity += (target.Center - Main.player[projectile.owner].Center) / 3f * target.knockBackResist;
        }

        public override void AI()
		{
            projectile.Center = Main.player[projectile.owner].Center;
            for (int i = 0; i < 6; i++)
            {
                Dust d = Dust.NewDustPerfect(Main.player[projectile.owner].Center + Main.rand.NextVector2CircularEdge(50, 50), 59);
                d.scale = projectile.penetrate / 15f;
                d.noGravity = true;
            }
            if (Main.player[projectile.owner].dead)
            {
                projectile.Kill();
            }
        }
    }
}