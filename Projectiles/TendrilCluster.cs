using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class TendrilCluster : ModProjectile
	{
        private int timer = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tendril Cluster");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 8;
			projectile.height = 8;
            projectile.tileCollide = false;
            projectile.netImportant = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 2;
            projectile.hide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).radiant = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 5;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.Center, true);
            if (Main.myPlayer == projectile.owner)
            {
                projectile.netUpdate = true;
                if (player.channel && !player.noItems && !player.CCed)
                {
                    projectile.timeLeft = 2;
                    projectile.Center = player.Center;

                    if (timer == 0)
                    {
                        timer = 5;
                        foreach (NPC target in Necromancy.NearbyNPCs(player.Center + (Main.MouseWorld - player.Center).SafeNormalize(Vector2.UnitX * player.direction) * 64f, 160f))
                        {
                            Projectile proj = Projectile.NewProjectileDirect(projectile.Center, (Main.MouseWorld - player.Center).SafeNormalize(Vector2.UnitX * player.direction), mod.ProjectileType<Tendril>(), projectile.damage, 0f, projectile.owner);
                            proj.netUpdate = true;
                            Tendril beam = (Tendril)proj.modProjectile;
                            beam.target = target;
                        }
                    }
                    else
                    {
                        timer--;
                    }
                }
                else
                {
                    projectile.Kill();
                }
            }
        }
    }
}