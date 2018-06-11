using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class HarmonyOrb : ModProjectile
	{
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
            Player owner = Main.player[projectile.owner];
            Player target = Main.player[(int)projectile.ai[0]];
            Lighting.AddLight(projectile.Center, new Vector3(1f, 1f, 0.4f));
            if (!target.dead && owner.channel && Vector2.Distance(projectile.Center, owner.Center) < 1000f)
            {
                if (projectile.timeLeft <= 1)
                {
                    owner.statLife--;
                    if (owner.statLife <= 0)
                    {
                        Terraria.DataStructures.PlayerDeathReason damageSource;
                        switch (Main.rand.Next(3))
                        {
                            case 0:
                                damageSource = Terraria.DataStructures.PlayerDeathReason.ByCustomReason(owner.name + " ran out of blood.");
                                break;
                            case 1:
                                damageSource = Terraria.DataStructures.PlayerDeathReason.ByCustomReason(owner.name + " couldn't handle the power.");
                                break;
                            default:
                                damageSource = Terraria.DataStructures.PlayerDeathReason.ByCustomReason(owner.name + " didn't watch their health bar.");
                                break;
                        }
                        owner.KillMe(damageSource, 5, -owner.direction);
                    }
                    projectile.timeLeft = 4;
                }
                projectile.Center = new Vector2(target.Center.X, target.position.Y - 24f);
                target.AddBuff(mod.BuffType<Buffs.HarmonyOrb>(), 2);
            }
            else
            {
                projectile.Kill();
            }
        }
    }
}