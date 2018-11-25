using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Necromancy.Projectiles
{
    public class InfernalRocket : ModProjectile
    {
        // rocket projectile, explodes on contact

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infernal Rocket");
        }

        public override void SetDefaults()
        {
            projectile.ranged = true;
            projectile.CloneDefaults(ProjectileID.RocketI);
            projectile.ranged = false;
			projectile.width = 14;
			projectile.height = 22;
			projectile.aiStyle = 34;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 120;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).burn = true;
        }

        public override void AI()
        {
            if (projectile.timeLeft < 3)
            {
                Explode();
            }
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            // Vanilla explosions do less damage to Eater of Worlds in expert mode, so we will too.
            if (Main.expertMode)
            {
                if (target.type >= NPCID.EaterofWorldsHead && target.type <= NPCID.EaterofWorldsTail)
                {
                    damage /= 5;
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            Explode();
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Explode();
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Explode();
            return false;
        }

        private void Explode()
        {
            if (projectile.ai[0] == 0f)
            {
                projectile.ai[0] = 1f;
                Vector2 center = projectile.Center;
                projectile.width = 64;
                projectile.height = 64;
                projectile.Center = center;
                projectile.velocity *= 0;
                projectile.timeLeft = 1;
                projectile.penetrate = -1;
                Main.PlaySound(SoundID.Item41, projectile.Center);
                Main.PlaySound(SoundID.Item14, projectile.Center);
                for (int i = 0; i < 20; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[dustIndex].velocity *= 1.4f;
                }
                for (int i = 0; i < 10; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
                    Main.dust[dustIndex].noGravity = true;
                    Main.dust[dustIndex].velocity *= 5f;
                }
                for (int g = 0; g < 3; g++)
                {
                    int goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 0.7f;
                    Main.gore[goreIndex].velocity = Main.gore[goreIndex].velocity;
                }
            }
        }
    }
}