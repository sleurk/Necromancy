using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class HarmonyOrb : ModProjectile
	{
        // stays over a targeted player and heals them
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orb of Harmony");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 16;
			projectile.height = 16;
            projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 4;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
        }

		public override void AI()
		{
            if (Main.myPlayer == projectile.owner)
            {
                projectile.netUpdate = true;
                Player owner = Main.player[projectile.owner];
                Player target = Main.player[(int)projectile.ai[0]];
                Lighting.AddLight(projectile.Center, new Vector3(1f, 1f, 0.4f));
                if (!target.dead && owner.channel && Vector2.DistanceSquared(projectile.Center, owner.Center) < 1000f * 1000f)
                {
                    if (projectile.timeLeft <= 1)
                    {
                        Necromancy.DrainLife(owner, 1);
                        projectile.timeLeft = 4;
                    }
                    projectile.Center = new Vector2(target.Center.X, target.position.Y - 24f);
                    target.AddBuff(mod.BuffType<Buffs.HarmonyOrb>(), 2);

                    Vector2 dustPos = projectile.Center + Main.rand.NextVector2Circular(projectile.width / 2, projectile.height / 2);
                    Dust d = Dust.QuickDust(dustPos, new Color(1f, 1f, 0.5f));
                    d.velocity = 0.08f * (target.Center - d.position) + target.velocity;
                }
                else
                {
                    projectile.Kill();
                }
            }
        }
    }
}