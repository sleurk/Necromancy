using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class StaticFlailBall : ModProjectile
	{
        // weird flail shot in groups of 3 that try to follow the mouse and make lines of damage to the player

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Static Flail");
        }

        public override void SetDefaults()
        {
            projectile.melee = true;
            projectile.width = 34;
			projectile.height = 34;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 300;
            projectile.netImportant = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).shock = true;
        }

		public override void AI()
        {
            float rotateSpeed = projectile.velocity.X;
            projectile.rotation += rotateSpeed + Main.rand.NextFloat(-0.2f, 0.2f);

            Player player = Main.player[projectile.owner];
            if (player.dead)
            {
                projectile.Kill();
            }

            Vector2 toPlayer = player.Center - projectile.Center;
            if (toPlayer.Length() > 1200f) projectile.Kill();
            if (player.channel)
            {
                projectile.timeLeft = 300;
            }
            else
            {
                projectile.velocity = projectile.velocity * 0.95f + toPlayer * 0.05f;
                if (toPlayer.Length() < 30f) projectile.Kill();
                projectile.tileCollide = false;
                return;
            }

            if (Main.myPlayer == projectile.owner)
            {
                if (toPlayer.Length() > 320f)
                {
                    projectile.velocity = projectile.velocity * 0.97f + toPlayer * 0.05f;
                }
                else
                {
                    Vector2 mouse = Main.MouseWorld;
                    Vector2 toMouse = (mouse - projectile.Center);
                    toMouse = toMouse.SafeNormalize(Vector2.Zero) * 1.5f;


                    projectile.velocity += toMouse + Main.rand.NextVector2Circular(0.5f, 0.5f);

                    projectile.netUpdate = true;
                }
            }

            Projectile nextBall = Main.projectile[(int)projectile.ai[0]];
            projectile.ai[1]++;
            if (projectile.ai[1] > 3.5f && player.active && !player.dead)
            {
                projectile.ai[1] = 0;
                Projectile proj = Projectile.NewProjectileDirect(projectile.Center, (player.Center - projectile.Center).SafeNormalize(Vector2.Zero),
                    mod.ProjectileType("SFlailBolt"), projectile.damage, 0f, projectile.owner, projectile.whoAmI, player.whoAmI);
                proj.timeLeft = (int)(player.Center - projectile.Center).Length();
                proj.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom = projectile.GetGlobalProjectile<NecromancyGlobalProjectile>().shotFrom;
            }
            Dust.QuickDust(projectile.Center + Main.rand.NextVector2Circular(16f, 16f), new Color(1f, 1f, 0.5f)).velocity = projectile.velocity * 0.4f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }
            projectile.velocity *= 0.5f;

            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Player player = Main.player[projectile.owner];
            if (player.active && !player.dead) ElectricBolt.PreDrawLightning(projectile.Center, player.Center, spriteBatch, mod);
            return true;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += (int)(projectile.velocity.Length() * 3f);
        }
    }
}