using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class EnvyRing : ModProjectile
	{
        // moves in a circle to collide with paired projectile
        // explodes on contact with other projectile
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ring Bolt");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 16;
			projectile.height = 16;
			projectile.friendly = true;
            projectile.tileCollide = false;
			projectile.penetrate = 3;
            projectile.netImportant = true;
			projectile.timeLeft = 39;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
        }

        public override void AI()
		{
            projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(5 * projectile.ai[0]));
            Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 11, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f).noGravity = true;
            if (projectile.timeLeft == 15)
            {
                Main.PlaySound(SoundID.Item20, projectile.Center);
            }
            if (projectile.timeLeft == 3)
            {
                projectile.hide = true;
                Vector2 oldC = projectile.Center;
                projectile.width = 128;
                projectile.height = 128;
                projectile.Center = oldC;
                projectile.velocity *= 0;
                projectile.timeLeft = 2;
                projectile.penetrate = -1;
                Main.PlaySound(SoundID.Item41, projectile.Center);
                for (int i = 0; i < 13; i++)
                {
                    Dust d = Dust.NewDustPerfect(projectile.Center, 61, Main.rand.NextVector2Circular(12f, 12f));
                    d.noGravity = true;
                    d.scale = Main.rand.NextFloat(1f, 2f);
                }
            }
        }

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 11, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
			}
			Main.PlaySound(SoundID.Item25, projectile.position);
		}
    }
}