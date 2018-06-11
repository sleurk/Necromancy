using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class Brick : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brick");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 12;
			projectile.height = 12;
            projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
            projectile.extraUpdates = 1;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).ranged = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).lifeSteal = 10;
        }

        public override void AI()
        {
            Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 148, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f).scale = 0.5f;
        }

        public override void Kill(int timeLeft)
		{
            projectile.velocity.Y += 0.1f;
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 148, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f).scale = 0.5f;
			}
			Main.PlaySound(0, projectile.position);
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!target.boss) target.GetGlobalNPC<NPCs.NecromancyNPC>().brick = 30;
        }
    }
}