using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
	public class CursedFireball : ModProjectile
	{
        // acts exactly the same as cursed flames projectile, shot differently
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cursed Fireball");
        }

        public override void SetDefaults()
        {
            projectile.SetDefaults(ProjectileID.CursedFlameFriendly);
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
            aiType = ProjectileID.CursedFlameFriendly;
        }
    }
}