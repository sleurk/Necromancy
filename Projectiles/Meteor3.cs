using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
    public class Meteor3 : ModProjectile
    {
        private bool exploded;
        private int dustType;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Meteor");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.aiStyle = 1;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            exploded = false;
            dustType = 135;
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
            projectile.velocity.Y += 0.1f;
            for (int i = 0; i < 3; i++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, dustType, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
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
            projectile.width = 100;
            projectile.height = 100;
            projectile.Center = center;
            projectile.velocity *= 0;
            projectile.timeLeft = 2;
            Main.PlaySound(SoundID.Item14, projectile.Center);
            for (int i = 0; i < 13; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustType, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            for (int g = 0; g < Main.rand.Next(2, 4); g++)
            {
                int goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (projectile.width / 2) - 24f, projectile.position.Y + (projectile.height / 2) - 24f), Main.rand.NextVector2CircularEdge(projectile.width / 80f, projectile.height / 80f), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 0.5f + Main.rand.NextFloat();
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
            }
        }
    }
}