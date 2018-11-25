using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public abstract class WandEnergy : ModProjectile
	{
        // base projectile for all (color)Energy projectiles, homes slightly on enemies
        protected virtual int Pierce
        {
            get { return 0; }
        }
        protected virtual int Heal
        {
            get { return 0; }
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.penetrate = Pierce;
            projectile.extraUpdates = 4;
            projectile.timeLeft = 600;
            ProjectileID.Sets.Homing[projectile.type] = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).radiant = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).healPower = Heal;
        }

		public override void AI()
        {
            Dust.QuickDust(projectile.Center, GetColor());
            NPC target = Necromancy.NearestNPC(projectile.Center, 256f, false, true);
            if (target != null)
            {
                projectile.velocity = 0.9995f * projectile.velocity + 0.0005f * (target.Center - projectile.Center);
                projectile.velocity = projectile.velocity.SafeNormalize(Vector2.Zero) * 3f;
            }
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item25, projectile.position);
		}

        protected virtual Color GetColor()
        {
            return default(Color);
        }
    }
}