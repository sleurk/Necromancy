using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class Frostbite : ModProjectile
	{
        // basic thrown projectile, gravity
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frostbite");
        }

        public override void SetDefaults()
        {
            projectile.thrown = true;
            projectile.width = 18;
			projectile.height = 36;
			projectile.friendly = true;
            projectile.extraUpdates = 3;
			projectile.penetrate = 1;
			projectile.timeLeft = 120;
            projectile.aiStyle = 1;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).ice = true;
        }

        public override void AI()
        {
            if (Main.rand.NextFloat() < 0.3f)
            {
                Dust d = Dust.QuickDust(projectile.Center, new Color(0.5f, 1f, 1f));
                d.velocity = 0.5f * projectile.velocity + Main.rand.NextVector2Circular(2f, 2f);
                d.alpha = 50;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust d = Dust.QuickDust(projectile.Center, Main.rand.NextBool() ? Color.Cyan : new Color(0.5f, 1f, 1f));
                d.velocity = 0.5f * projectile.velocity + Main.rand.NextVector2Circular(2f, 2f);
                d.alpha = 50;
            }
            Main.PlaySound(0, projectile.position);
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 120);
        }
    }
}