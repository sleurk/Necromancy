using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles.Minions
{
	public class CelestialPixie : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Pixie");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            Main.projFrames[projectile.type] = 8;
            ProjectileID.Sets.Homing[projectile.type] = true;
            projectile.width = 54;
			projectile.height = 34;
			projectile.friendly = true;
			projectile.timeLeft = 18000;
            projectile.aiStyle = 54; // flying contact minion, I forgot which one
            projectile.penetrate = -1;
			projectile.ignoreWater = true;
            projectile.netImportant = true;
			projectile.tileCollide = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).summon = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).summonCost = 20;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 30;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (!player.dead)
            {
                projectile.timeLeft = 2;
            }

            // animation
            projectile.frameCounter++;
            if (projectile.frameCounter > 7)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame > 7)
                {
                    projectile.frame = 0;
                }
            }
            Lighting.AddLight(projectile.position, 0.6f, 0f, 0.9f);
        }
    }
}
