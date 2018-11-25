using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
    public class MeteorEye : ModProjectile
    {
        // large meteor
        // explodes on contact

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Meteor");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            // thorium.radiant = true
            projectile.width = 54;
            projectile.height = 54;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.tileCollide = false;
            projectile.extraUpdates = 6;
            projectile.aiStyle = 0;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).burn = true;
        }

        public override void AI()
        {
            for (int i = 0; i < 3; i++)
            {
                Dust.NewDustPerfect(projectile.Center + Main.rand.NextVector2Circular(projectile.width / 2f, projectile.height / 2f), 6, Vector2.Zero, 0,
                    default(Color), Main.rand.NextFloat(0.8f, 3f)).noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            if (projectile.ai[0] == 0f) Explode();
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[0] == 0f) Explode();
        }

        private void Explode()
        {
            projectile.ai[0] = 1f;
            Vector2 center = projectile.Center;
            projectile.width = 150;
            projectile.height = 150;
            projectile.hide = true;
            projectile.Center = center;
            projectile.velocity *= 0;
            projectile.timeLeft = 2;
            Main.PlaySound(SoundID.Item14, projectile.Center);
            for (int i = 0; i < 30; i++)
            {
                Dust d = Dust.NewDustDirect(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
                d.velocity = (d.position - projectile.Center) * 0.1f;
                d.noGravity = true;
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