using Necromancy.Empowerments;
using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class KongaPulse : ModProjectile
	{
        // dummy projectile to hit all enemies in range
        // can stun if it is the seventh pulse
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Konga Pulse");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 16;
			projectile.height = 16;
			projectile.friendly = true;
			projectile.penetrate = 3;
			projectile.timeLeft = 2;
            projectile.hide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).symphonic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).empowermentType = EmpType.LifeRegen;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[0] == 1f)
            {
                target.AddBuff(mod.BuffType("Stunned"), 60);
            }
        }
    }
}