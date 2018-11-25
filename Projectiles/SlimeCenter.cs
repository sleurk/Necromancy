using Necromancy.Empowerments;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class SlimeCenter : ModProjectile
	{
        // basic projectile, SlimeOrbiters will orbit this
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slime Mass");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 40;
			projectile.height = 40;
			projectile.friendly = true;
			projectile.penetrate = -1;
            projectile.hide = true;
			projectile.timeLeft = 1200;
            projectile.tileCollide = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 30;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).goo = true;
        }

		public override void AI()
		{
            NPC target = Necromancy.NearestNPC(projectile.Center, 1200f, false, false);
            if (target != null)
            {
                projectile.velocity += (target.Center - projectile.Center).SafeNormalize(Vector2.Zero) * 0.25f;
                projectile.velocity = projectile.velocity.SafeNormalize(Vector2.Zero) * 3f;
            }
            for (int k = 0; k < 5; k++)
            {
                Dust d = Dust.QuickDust(projectile.Center + Main.rand.NextVector2Circular(projectile.width / 2, projectile.height / 2), new Color(0.5f, 1f, 0.5f));
                d.velocity = (projectile.Center - d.position).RotatedBy(0.5) * 0.1f;
            }
        }
    }
}