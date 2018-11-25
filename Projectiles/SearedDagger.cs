using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class SearedDagger : ModProjectile
	{
        // basic short-range projectile
        // shot in a spread
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Seared Dagger");
        }

        public override void SetDefaults()
        {
            projectile.thrown = true;
            projectile.width = 18;
			projectile.height = 36;
			projectile.friendly = true;
            projectile.extraUpdates = 1;
			projectile.penetrate = 1;
			projectile.timeLeft = 30;
            projectile.aiStyle = 0;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).burn = true;
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (Main.rand.NextFloat() < 0.5f)
            {
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 6).noGravity = true;
                Dust d = Dust.QuickDust(projectile.Center, new Color(0.5f, 0.3f, 0f));
                d.velocity = 0.5f * projectile.velocity + Main.rand.NextVector2Circular(2f, 2f);
                d.alpha = 50;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust d = Dust.QuickDust(projectile.Center, Main.rand.NextBool() ? Color.Red : new Color(0.5f, 0.3f, 0f));
                d.velocity = 0.5f * projectile.velocity + Main.rand.NextVector2Circular(2f, 2f);
                d.alpha = 50;
            }
            Main.PlaySound(0, projectile.position);
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 120);
        }
    }
}