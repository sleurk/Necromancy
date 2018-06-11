using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Achievements;

namespace Necromancy.Projectiles
{
	public class HeartBomb : ModProjectile
	{
        private bool exploded;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heart Bomb");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 120;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).throwing = true;
            exploded = false;
        }

		public override void AI()
		{
            projectile.rotation += projectile.velocity.X / 30f;
            projectile.velocity.Y += 0.3f;
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 12, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
			}
		}

        public override void Kill(int timeLeft)
        {
            if (!exploded) Explode();
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!exploded) Explode();
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X / 1.2f;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y / 3f;
            }
            projectile.velocity.X *= 0.99f;
            return false;
        }

        private void Explode()
        {
            ExplodeTiles();
            exploded = true;
            Vector2 center = projectile.Center;
            projectile.width = 300;
            projectile.height = 300;
            projectile.Center = center;
            projectile.velocity *= 0;
            projectile.timeLeft = 2;
            Main.PlaySound(SoundID.Item41, projectile.Center);
            for (int i = 0; i < 13; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            for (int g = 0; g < 3; g++)
            {
                int goreIndex = Gore.NewGore(projectile.Center, Main.rand.NextVector2CircularEdge(projectile.width / 160f, projectile.height / 160f), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 0.5f + Main.rand.NextFloat();
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
            }
        }

        public void ExplodeTiles()
        {
            // examplemod
            int explosionRadius = 2;
            int minTileX = (int)(projectile.position.X / 16f - (float)explosionRadius);
            int maxTileX = (int)(projectile.position.X / 16f + (float)explosionRadius);
            int minTileY = (int)(projectile.position.Y / 16f - (float)explosionRadius);
            int maxTileY = (int)(projectile.position.Y / 16f + (float)explosionRadius);
            if (minTileX < 0)
            {
                minTileX = 0;
            }
            if (maxTileX > Main.maxTilesX)
            {
                maxTileX = Main.maxTilesX;
            }
            if (minTileY < 0)
            {
                minTileY = 0;
            }
            if (maxTileY > Main.maxTilesY)
            {
                maxTileY = Main.maxTilesY;
            }
            bool canKillWalls = false;
            for (int x = minTileX; x <= maxTileX; x++)
            {
                for (int y = minTileY; y <= maxTileY; y++)
                {
                    float diffX = Math.Abs((float)x - projectile.position.X / 16f);
                    float diffY = Math.Abs((float)y - projectile.position.Y / 16f);
                    double distance = Math.Sqrt((double)(diffX * diffX + diffY * diffY));
                    if (distance < (double)explosionRadius && Main.tile[x, y] != null && Main.tile[x, y].wall == 0)
                    {
                        canKillWalls = true;
                        break;
                    }
                }
            }
            AchievementsHelper.CurrentlyMining = true;
            for (int i = minTileX; i <= maxTileX; i++)
            {
                for (int j = minTileY; j <= maxTileY; j++)
                {
                    float diffX = Math.Abs((float)i - projectile.position.X / 16f);
                    float diffY = Math.Abs((float)j - projectile.position.Y / 16f);
                    double distanceToTile = Math.Sqrt((double)(diffX * diffX + diffY * diffY));
                    if (distanceToTile < (double)explosionRadius)
                    {
                        bool canKillTile = true;
                        if (Main.tile[i, j] != null && Main.tile[i, j].active())
                        {
                            canKillTile = true;
                            if (Main.tileDungeon[(int)Main.tile[i, j].type] || Main.tile[i, j].type == 88 || Main.tile[i, j].type == 21 || Main.tile[i, j].type == 26 || Main.tile[i, j].type == 107 || Main.tile[i, j].type == 108 || Main.tile[i, j].type == 111 || Main.tile[i, j].type == 226 || Main.tile[i, j].type == 237 || Main.tile[i, j].type == 221 || Main.tile[i, j].type == 222 || Main.tile[i, j].type == 223 || Main.tile[i, j].type == 211 || Main.tile[i, j].type == 404)
                            {
                                canKillTile = false;
                            }
                            if (!Main.hardMode && Main.tile[i, j].type == 58)
                            {
                                canKillTile = false;
                            }
                            if (!TileLoader.CanExplode(i, j))
                            {
                                canKillTile = false;
                            }
                            if (canKillTile)
                            {
                                WorldGen.KillTile(i, j, false, false, false);
                                if (!Main.tile[i, j].active() && Main.netMode != 0)
                                {
                                    NetMessage.SendData(17, -1, -1, null, 0, (float)i, (float)j, 0f, 0, 0, 0);
                                }
                            }
                        }
                        if (canKillTile)
                        {
                            for (int x = i - 1; x <= i + 1; x++)
                            {
                                for (int y = j - 1; y <= j + 1; y++)
                                {
                                    if (Main.tile[x, y] != null && Main.tile[x, y].wall > 0 && canKillWalls && WallLoader.CanExplode(x, y, Main.tile[x, y].wall))
                                    {
                                        WorldGen.KillWall(x, y, false);
                                        if (Main.tile[x, y].wall == 0 && Main.netMode != 0)
                                        {
                                            NetMessage.SendData(17, -1, -1, null, 2, (float)x, (float)y, 0f, 0, 0, 0);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            AchievementsHelper.CurrentlyMining = false;
        }
    }
}