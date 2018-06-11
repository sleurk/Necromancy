using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class AquaticDrumBeat : ModProjectile
	{
        int dustType = 60;
        bool exploded = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquatic Drum Beat");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
            projectile.hide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).symphonic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).buffType = mod.BuffType<Buffs.EmpowermentMeleeDamage>();
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            base.PostDraw(spriteBatch, lightColor);
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
            Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, dustType).noGravity = true;
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
            projectile.width = 120;
            projectile.height = 120;
            projectile.Center = center;
            projectile.velocity *= 0;
            projectile.timeLeft = 2;
            Main.PlaySound(SoundID.Item14, projectile.Center);
            for (int i = 0; i < 20; i++)
            {
                int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
                Main.dust[dustIndex].noGravity = true;
            }
        }
    }
}