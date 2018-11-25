using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public abstract class PlasmBlast : ModProjectile
	{
        // creates a horizontal line of damage/healing(?) from player
        // definitely a copy of Fireball spell from Necrodancer
        // different files are different frames of animation because i dont really know why????

        // NYI because no direct thorium integration but this will be a healing projectile

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plasm");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            // thorium.radiant
            projectile.width = 48;
			projectile.height = 48;
			projectile.friendly = true;
            projectile.penetrate = -1;
			projectile.timeLeft = 21;
            projectile.ignoreWater = true;
			projectile.tileCollide = false;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).blood = true;
        }

        public override void AI()
        {
            if (projectile.timeLeft % 3 == 0) projectile.frame++;
            projectile.Center = Main.player[projectile.owner].Center + Vector2.UnitX * projectile.ai[0];
            Lighting.AddLight(projectile.Center, projectile.timeLeft / 21f, 0f, 0f);
        }
        
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 29;
        }
    }
}
