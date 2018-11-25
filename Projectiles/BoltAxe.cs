using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class BoltAxe : ModProjectile
	{
        // moves in a curvy shape and shoots lightning towards the player
        private float RotationOffset
        {
            get { return projectile.ai[0]; }
            set { projectile.ai[0] = value; }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bolt Axe");
        }

        public override void SetDefaults()
        {
            projectile.thrown = true;
            projectile.width = 36;
			projectile.height = 34;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 90;
            projectile.tileCollide = false;
            projectile.netImportant = true;
            projectile.alpha = 100;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shock = true;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.velocity = projectile.velocity * 0.999f + (player.Center - projectile.Center) * 0.001f;

            projectile.rotation += 0.42f;
            
            projectile.ai[1]++;
            if (projectile.ai[1] > 3.5f && player.active && !player.dead)
            {
                projectile.ai[1] = 0;
                Projectile proj = Projectile.NewProjectileDirect(projectile.Center, (player.Center - projectile.Center).SafeNormalize(Vector2.Zero),
                    mod.ProjectileType("BAxeBolt"), projectile.damage, 0f, projectile.owner, projectile.whoAmI, player.whoAmI);
                proj.timeLeft = (int)(player.Center - projectile.Center).Length();
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = projectile.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Player player = Main.player[projectile.owner];
            if (player.active && !player.dead) ElectricBolt.PreDrawLightning(projectile.Center, player.Center, spriteBatch, mod);
            return true;
        }
    }
}
