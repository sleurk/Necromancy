using Microsoft.Xna.Framework;
using Necromancy.NPCs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class ElectricWhip : ModProjectile
	{
        // whip projectile
        // fast-moving, straight line, short range
        private const int LIFESPAN = 90;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Electric Whip");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 4;
			projectile.height = 4;
            projectile.friendly = true;
            projectile.penetrate = 1;
			projectile.timeLeft = LIFESPAN;
            projectile.hide = true;
            projectile.extraUpdates = 20;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).melee = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).lifeSteal = 10;

        }

		public override void AI()
		{
            for (int i = 0; i < 4; i++)
            { 
                Dust d = Dust.QuickDust(projectile.Center + projectile.velocity * i / 4f, new Color(0.3f, 1f, 0.1f));
                d.noGravity = true;
                d.scale = Main.rand.NextFloat();
                d.velocity *= 0.2f;
            }
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (target.GetGlobalNPC<NecromancyNPC>().lightningHit) return false;
            return base.CanHitNPC(target);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.GetGlobalNPC<NecromancyNPC>().lightningHit)
            {
                projectile.penetrate++;
            }
            else
            {
                target.GetGlobalNPC<NecromancyNPC>().lightningHit = true;
                if (projectile.ai[1] != 0)
                {
                    for (int i = 0; i < projectile.ai[0]; i++)
                    {
                        float radius = projectile.velocity.Length() * LIFESPAN;

                        NPC newTarget = Necromancy.NearestNPC(projectile.position + projectile.velocity * 32f, radius / 2, true);
                        if (newTarget != null)
                        {
                            Vector2 toNewTarget = newTarget.Center - projectile.Center;
                            toNewTarget.Normalize();
                            toNewTarget *= projectile.velocity.Length();

                            Projectile bolt = Projectile.NewProjectileDirect(projectile.Center, toNewTarget, projectile.type, damage, projectile.knockBack, projectile.owner, projectile.ai[0], projectile.ai[1] - 1);
                            newTarget.GetGlobalNPC<NecromancyNPC>().lightningHit = true;

                            for (int j = 0; j < 4; j++)
                            {
                                Dust d = Dust.QuickDust(bolt.Center + bolt.velocity * j / 4f, new Color(0.3f, 1f, 0.1f));
                                d.noGravity = true;
                                d.scale = Main.rand.NextFloat();
                                d.velocity *= 0.2f;
                            }
                        }
                    }
                    projectile.Kill();
                }
            }
        }
    }
}