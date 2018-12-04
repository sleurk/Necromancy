using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class Pylon : ModProjectile
	{
        // waits until active
        // creates a line of damage between the set of pylons

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pylon");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 28;
			projectile.height = 28;
			projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.timeLeft = 600;
            projectile.netImportant = true;
        }

        public override void AI()
        {
            Dust.QuickDust(projectile.Center, new Color(1f, 0.9f, 0.5f)).velocity = Main.rand.NextVector2CircularEdge(2f, 2f);
            projectile.rotation += MathHelper.ToRadians(2);
            if (Main.player[projectile.owner].ownedProjectileCounts[mod.ProjectileType("PylonCreator")] > 0)
            {
                // waiting to be activated
                projectile.timeLeft = 600;
            }
            else if (projectile.ai[1] >= 0f)
            {
                if (projectile.timeLeft % 4 == 0)
                {
                    Vector2 other = new Vector2(projectile.ai[0], projectile.ai[1]);
                    Vector2 toOther = other - projectile.Center;
                    if (Main.player[projectile.owner].ownedProjectileCounts[mod.ProjectileType("PylonCreator")] == 0
                     && other != new Vector2(-1f, -1f) && toOther.LengthSquared() < 600f * 600f)
                    {
                        toOther = toOther.SafeNormalize(Vector2.Zero);
                        Projectile proj = Projectile.NewProjectileDirect(projectile.Center, toOther, mod.ProjectileType("PHarmBolt"), projectile.damage, projectile.knockBack, projectile.owner);
                        proj.timeLeft = (int)(other - projectile.Center).Length();
                        proj.netUpdate = true;
                    }
                }
            }
        }

        public override bool PreDrawExtras(SpriteBatch spriteBatch)
        {
            if (projectile.ai[1] >= 0f)
            {
                Vector2 other = new Vector2(projectile.ai[0], projectile.ai[1]);
                Vector2 toOther = other - projectile.Center;
                if (Main.player[projectile.owner].ownedProjectileCounts[mod.ProjectileType("PylonCreator")] == 0
                 && other != new Vector2(-1f, -1f) && toOther.LengthSquared() < 600f * 600f)
                {
                    ElectricBolt.PreDrawLightning(projectile.Center, other, spriteBatch, mod);
                }
            }
            return true;
        }
    }
}