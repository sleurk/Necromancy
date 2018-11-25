using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
    public class RotRain : ModProjectile
    {
        // rain from rot nimbus rod clone
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rot Rain");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 2;
            projectile.height = 40;
            projectile.aiStyle = 45;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.timeLeft = 60;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.penetrate = -1;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).magic = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Venom, 600);
        }
    }
}
