using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class CelestialCrystalShard : ModProjectile
	{
        // basic thrown projectile, like throwing knives
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Crystal Shard");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 12;
			projectile.height = 22;
			projectile.friendly = true;
			projectile.penetrate = 10;
			projectile.timeLeft = 120;
            projectile.aiStyle = 2;
            aiType = ProjectileID.ThrowingKnife;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).throwing = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 62, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
            Main.PlaySound(SoundID.Item27, projectile.position);
            return true;
        }
    }
}