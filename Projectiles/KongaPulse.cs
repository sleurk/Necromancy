using Terraria;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class KongaPulse : ModProjectile
	{
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
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).buffType = mod.BuffType<Buffs.EmpowermentRegen>();
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[0] == 1f)
            {
                target.AddBuff(32, 60 * 4);
            }
        }
    }
}