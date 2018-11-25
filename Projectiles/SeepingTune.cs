using Necromancy.Empowerments;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class SeepingTune : ModProjectile
	{
        // weird bouncy projectile
        // when hitting an enemy it shoots straight up to try to bounce on the enemy
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Seeping Tune");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 6;
			projectile.height = 12;
			projectile.friendly = true;
			projectile.penetrate = 10;
			projectile.timeLeft = 2400;
            projectile.hide = true;
            projectile.extraUpdates = 5;
            projectile.aiStyle = 1;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).symphonic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).empowermentType = EmpType.LifeSteal;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }
            projectile.penetrate--;
            projectile.damage += 10;
            Dust.NewDustPerfect(projectile.Center, 135, projectile.velocity / 4f).noGravity = true;
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 4;
            projectile.velocity = -Vector2.UnitY * projectile.velocity.Length();
            projectile.damage += 10;
        }

        public override void AI()
		{
            projectile.velocity.Y += 0.05f;
            for (int k = 0; k < 15; k++)
            {
                Dust.NewDustPerfect(projectile.Center, 135, projectile.velocity / 4f).noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 20; k++)
            {
                Dust d = Dust.NewDustPerfect(projectile.Center, 135, Main.rand.NextVector2Circular(8f, 8f));
                d.scale = Main.rand.NextFloat() + 1f;
                d.noGravity = true;
            }
		}
    }
}