using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class CryonicJavelin : ModProjectile
	{
        // thrown spear projectile, leaves a trail of more damage in the air
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cryonic Javelin");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 14;
			projectile.height = 14;
			projectile.friendly = true;
			projectile.penetrate = -1;
            projectile.extraUpdates = 1;
			projectile.timeLeft = 180;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).throwing = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).ice = true;
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
            projectile.velocity.Y += 0.2f;
            if (projectile.timeLeft % 6 == 0)
            {
                Projectile.NewProjectileDirect(projectile.Center, projectile.velocity * 0.7f, mod.ProjectileType("CryonicTrail"), projectile.damage, 0f, projectile.owner);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 80, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
            Main.PlaySound(0, projectile.position);
            return true;
        }
    }
}