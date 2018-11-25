using Microsoft.Xna.Framework;
using Necromancy.NPCs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles.Minions
{
	public class SummonLightning : ModProjectile
	{
        // lightning bolt shot by small follower lightning cloud
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 8;
			projectile.height = 8;
            projectile.friendly = true;
            projectile.penetrate = 1;
			projectile.timeLeft = 300;
            projectile.hide = true;
            projectile.extraUpdates = 100;
            ProjectileID.Sets.Homing[projectile.type] = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).summon = true;

        }

		public override void AI()
        {
            for (int i = 0; i < 4; i++)
            {
                Dust d = Dust.QuickDust(projectile.Center + projectile.velocity * i / 4f, new Color(1f, 1f, 0f));
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
                        NPC newTarget = Necromancy.NearestNPC(projectile.position, 500f, true);
                        if (newTarget != null)
                        {
                            Vector2 toNewTarget = newTarget.Center - projectile.Center;
                            toNewTarget.Normalize();
                            toNewTarget *= projectile.velocity.Length();

                            Projectile bolt = Projectile.NewProjectileDirect(projectile.Center, toNewTarget, projectile.type, damage, projectile.knockBack, projectile.owner, projectile.ai[0] - 1, projectile.ai[1] - 1);
                            newTarget.GetGlobalNPC<NecromancyNPC>().lightningHit = true;
                            for (int j = 0; j < 4; j++)
                            {
                                Dust d = Dust.QuickDust(projectile.Center + projectile.velocity * j / 4f, new Color(1f, 1f, 0f));
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