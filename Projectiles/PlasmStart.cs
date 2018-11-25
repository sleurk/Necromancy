using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class PlasmStart : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plasm");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            // projectile.radiant = true
            projectile.width = 48;
			projectile.height = 48;
			projectile.friendly = true;
			projectile.timeLeft = 21;
            Main.projFrames[mod.ProjectileType("PlasmStart")] = 7;
            projectile.ignoreWater = true;
			projectile.tileCollide = false;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).blood = true;
        }

        public override void AI()
        {
            if (projectile.timeLeft % 3 == 0) projectile.frame++;
            projectile.Center = Main.player[projectile.owner].Center;
        }

        // direct heal or no?
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 29;
        }
    }
}
