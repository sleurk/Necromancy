using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles.Minions
{
	public class SummonLightning : ModProjectile
	{

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
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).summon = true;

        }

		public override void AI()
		{
            // code from vanilla
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] > 9f)
            {
                int num3;
                for (int num452 = 0; num452 < 4; num452 = num3 + 1)
                {
                    Vector2 vector36 = projectile.position;
                    vector36 -= projectile.velocity * ((float)num452 * 0.25f);
                    projectile.alpha = 255;
                    int num453 = Dust.NewDust(vector36, 1, 1, 57);
                    Main.dust[num453].position = vector36;
                    Main.dust[num453].noGravity = true;
                    Main.dust[num453].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                    Dust dust3 = Main.dust[num453];
                    dust3.velocity *= 0.2f;
                    num3 = num452;
                }
            }
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (target.GetGlobalNPC<NPCs.NecromancyNPC>().lightningHit) return false;
            return base.CanHitNPC(target);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.GetGlobalNPC<NPCs.NecromancyNPC>().lightningHit)
            {
                projectile.penetrate++;
            }
            else
            {
                target.GetGlobalNPC<NPCs.NecromancyNPC>().lightningHit = true;
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

                            Projectile bolt = Projectile.NewProjectileDirect(projectile.position, toNewTarget, projectile.type, damage, projectile.knockBack, projectile.owner, projectile.ai[0] - 1, projectile.ai[1] - 1);
                            newTarget.GetGlobalNPC<NPCs.NecromancyNPC>().lightningHit = true;
                        }
                    }
                    projectile.Kill();
                }
            }
        }
    }
}