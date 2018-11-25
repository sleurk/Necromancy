using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Projectiles
{
    public class GooRain : ModProjectile
    {
        // rain from goo nimbus rod clone
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Goo Rain");
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
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).necrotic = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).radiant = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).goo = true;
            projectile.GetGlobalProjectile<NecromancyGlobalProjectile>(mod).healPower = 1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 30;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Goo"), 300);
        }
    }
}
