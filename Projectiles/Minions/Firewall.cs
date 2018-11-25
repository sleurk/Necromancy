using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles.Minions
{
	public class Firewall : ModProjectile
	{
        // NYI - summons are weird
        // This is supposed to be a sentry that creates a line of damage drawn by the player
        public Projectile other;

        protected Player Owner
        {
            get { return Main.player[projectile.owner]; }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Firewall");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
			projectile.height = 30;
			projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.netImportant = true;
			projectile.penetrate = 1;
			projectile.timeLeft = Projectile.SentryLifeTime;
        }

        public override void AI()
        {
            if (!Owner.dead && other != null && other.modProjectile is Firewall && other.active && TypeMatch() && other.owner == projectile.owner)
            {
                Firewall otherMP = (Firewall)other.modProjectile;
                otherMP.other = projectile;
            }
            else
            {
                projectile.Kill();
            }
            
            Vector2 toOther = (other.Center - projectile.Center).SafeNormalize(Vector2.Zero);

            projectile.rotation = toOther.ToRotation();

            Dust.NewDustPerfect(projectile.Center + toOther.RotatedBy(MathHelper.PiOver2) * (Main.rand.NextBool() ? 8f : -8f), 6, toOther * 3f).noGravity = true;
        }

        protected bool TypeMatch()
        {
            return other.type == mod.ProjectileType("FirewallPrime");
        }
    }
}