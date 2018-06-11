using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles.Minions
{
    public class SummonSnow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snow");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 2;
            projectile.height = 40;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.timeLeft = 360;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).summon = true;
        }

        public override void AI()
        {
            projectile.rotation += 0.1f;
            Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), 0f, 0.4f, 0.4f);
            projectile.velocity.Y += 0.43f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Chilled, 600);
            target.AddBuff(BuffID.Frostburn, 60);
        }
    }
}
