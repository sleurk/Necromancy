using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class PoisonedJavelin : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Poisoned Javelin");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 10;
			projectile.height = 10;
			projectile.friendly = true;
			projectile.penetrate = 3;
            projectile.aiStyle = 1;
			projectile.timeLeft = 120;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).throwing = true;
        }

        public override void AI()
        {
            if (projectile.timeLeft < 90)
            {
                projectile.velocity = projectile.velocity * 0.97f + Vector2.UnitY;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 300);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 39, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
            Main.PlaySound(0, projectile.position);
            return true;
        }
    }
}