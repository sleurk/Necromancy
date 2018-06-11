using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class LuteBlast : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lute Blast");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 100;
			projectile.height = 100;
			projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.netImportant = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 1;
            projectile.hide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).symphonic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).buffType = mod.BuffType<Buffs.EmpowermentImmortality>();
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 5;
        }

        public override void AI()
		{
            for (int i = 0; i < projectile.ai[0] * 15; i++)
            {
                Dust.NewDustPerfect(Main.rand.NextVector2Circular(50, 50) + projectile.Center, 21, Vector2.Zero).noGravity = true;
            }
        }
    }
}