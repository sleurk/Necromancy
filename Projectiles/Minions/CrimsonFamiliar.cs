using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles.Minions
{
	public class CrimsonFamiliar : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimson Familiar");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.netImportant = true;
			projectile.width = 128;
			projectile.height = 128;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 2;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
            projectile.alpha = 100;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).summon = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).blood = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).summonCost = 25;
        }

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			if (!player.dead)
			{
				projectile.timeLeft = 2;
			}
            for (int i = 0; i < 5; i++) Dust.NewDustPerfect(projectile.Center + Main.rand.NextVector2Circular(64f, 64f), 5, Vector2.Zero);
            Vector2 pos = new Vector2(projectile.ai[0], projectile.ai[1]);

            // projectile.ai[0] and projectile.ai[1] make the relative position to the player it was created at
            projectile.velocity = 0.05f * (pos + Main.player[projectile.owner].Center - projectile.Center);
        }
    }
}