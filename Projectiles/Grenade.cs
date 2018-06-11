using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class Grenade : ModProjectile
	{
        private bool exploded;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grenade");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 16;
			projectile.height = 16;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 300;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).ranged = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).lifeSteal = 30;
            exploded = false;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Main.expertMode)
            {
                if (target.type >= NPCID.EaterofWorldsHead && target.type <= NPCID.EaterofWorldsTail)
                {
                    damage /= 5;
                }
            }
        }

        public override void AI()
		{

            projectile.rotation += projectile.direction / 3f;
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
            if (!exploded) Explode();
            return false;
        }

        private void Explode()
        {
            exploded = true;
            Vector2 center = projectile.Center;
            projectile.width = 300 - projectile.timeLeft;
            projectile.height = 300 - projectile.timeLeft;
            projectile.Center = center;
            projectile.velocity *= 0;
            projectile.timeLeft = 2;
            Main.PlaySound(SoundID.Item41, projectile.Center);
            for (int i = 0; i < 13; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            for (int g = 0; g < Main.rand.Next(4, 7); g++)
            {
                int goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (projectile.width / 2) - 24f, projectile.position.Y + (projectile.height / 2) - 24f), Main.rand.NextVector2CircularEdge(projectile.width / 80f, projectile.height / 80f), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 0.5f + Main.rand.NextFloat();
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
            }
        }
    }
}