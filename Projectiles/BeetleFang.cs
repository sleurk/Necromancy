using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class BeetleFang : ModProjectile
	{
        // basic thrown projectile with gravity
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Beetle Fang");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.magic = true;
            projectile.width = 12;
			projectile.height = 18;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 120;
            projectile.aiStyle = 2;
            aiType = ProjectileID.ThrowingKnife;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).throwing = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Venom, 300);
        }

        public override void AI()
        {
            Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 179, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 179, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
            Main.PlaySound(0, projectile.position);
        }
    }
}