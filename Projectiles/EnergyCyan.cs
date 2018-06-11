using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class EnergyCyan : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cyan Energy");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.penetrate = 1;
            projectile.extraUpdates = 4;
            projectile.timeLeft = 600;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).radiant = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).healPower = 1;
        }

		public override void AI()
        {
            Dust.QuickDust(projectile.Center, new Color(0, .8f, .85f));
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
            {
                Dust.QuickDust(projectile.Center, new Color(0, .8f, .85f));
            }
			Main.PlaySound(SoundID.Item25, projectile.position);
		}
    }
}