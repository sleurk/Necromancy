using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class TridentBlast : ModProjectile
    {
        // projectile that is shot in threes
        // middle projectile moves straight
        // other projectiles curve inwards
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Trident Blast");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.magic = true;
            projectile.width = 14;
			projectile.height = 10;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 60;
            projectile.aiStyle = 0;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).melee = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).burn = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).lifeSteal = 1;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 1200);
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation();
            if (projectile.timeLeft <= 45)
            {
                Vector2 shootVel = new Vector2(projectile.ai[0], projectile.ai[1]);
                projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(-1.5f) * projectile.ai[0]);
            }
            Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 127, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f).noGravity = true;
            projectile.rotation = projectile.velocity.ToRotation();
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 127, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f).noGravity = true;
            }
            Main.PlaySound(0, projectile.position);
        }
    }
}