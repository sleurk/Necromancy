using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class ArcArrow : ModProjectile
	{
        // shoots lightning from this projectile to its paired projectile
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arc Arrow");
        }

        public override void SetDefaults()
        {
            projectile.ranged = true;
            projectile.width = 4;
			projectile.height = 4;
            projectile.aiStyle = 1;
            projectile.penetrate = 4;
			projectile.friendly = true;
            projectile.tileCollide = false;
			projectile.timeLeft = 120;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < 6; i++)
            {
                Vector2 pos = new Vector2(target.Center.X, target.position.Y - i * 16f);
                Vector2 vel = new Vector2(0f, 1f);
                Projectile proj = Projectile.NewProjectileDirect(pos, vel, mod.ProjectileType("ArcArrowCurrent"), projectile.damage, 0f, projectile.owner);
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = projectile.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom;
            }
        }

        public override void Kill(int timeLeft)
		{
			Main.PlaySound(0, projectile.position);
        }
    }
}