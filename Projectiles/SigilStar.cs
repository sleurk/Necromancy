using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
    public class SigilStar : ModProjectile
    {
        // weird circle projectile that makes a random star for visuals
        readonly int radius = 72;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SigilStar");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = (int)(radius * 2.6f);
            projectile.height = (int)(radius * 2.6f);
            projectile.friendly = true;
            projectile.hide = true;
            projectile.netImportant = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 2;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
        }

        public override void AI()
        {
            if (projectile.ai[0] == 0f) Explode();
        }

        private void Explode()
        {
            projectile.ai[0] = 1f;
            CreateStar();
        }

        private void CreateStar()
        {
            DustCircle();
            DustLines();
        }

        private void DustCircle()
        {
            for (int i = 0; i < radius * 2; i += 2)
            {
                CreateDust(projectile.Center + new Vector2(radius, 0).RotatedBy(MathHelper.ToRadians(i * 180 / radius)));
            }
        }

        private void DustLines()
        {
            int numPoints = Main.rand.Next(5, 12);
            float pointJumpHalf = numPoints / 2f;
            int pointJump = 0;
            while (pointJump == 0 || pointJump == pointJumpHalf)
            {
                pointJump = Main.rand.Next(2, numPoints - 1);
            }
            Vector2[] points = new Vector2[numPoints];
            points[0] = projectile.Center + new Vector2(radius, 0).RotatedByRandom(MathHelper.TwoPi);
            for (int i = 1; i < numPoints; i++)
            {
                points[i] = projectile.Center + (points[i - 1] - projectile.Center).RotatedBy(MathHelper.ToRadians(360f / numPoints));
            }
            for (int i = 0; i < numPoints; i++)
            {
                for (int j = 0; j < radius / 3; j++)
                {
                    CreateDustLine(points[i], points[(i + pointJump) % numPoints]);
                }
            }
        }

        private void CreateDust(Vector2 pos)
        {
            Dust d = Dust.QuickDust(pos, new Color(255, 200, 127));
            d.velocity = (d.position - projectile.Center).RotatedBy(MathHelper.ToRadians(90 * projectile.ai[0])) / projectile.ai[1];
            d.velocity += (projectile.Center - d.position) / 9f;
        }

        private void CreateDustLine(Vector2 pos1, Vector2 pos2)
        {
            Vector2 unit = pos2 - pos1;
            float dist = unit.Length();
            unit.Normalize();
            for (int i = 0; i < dist; i += 8)
            {
                CreateDust(pos1 + unit * i);
            }
        }
    }
}