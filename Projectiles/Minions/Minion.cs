using Terraria.ModLoader;

namespace Necromancy.Projectiles.Minions
{
    public abstract class Minion : ModProjectile
    {
        // base minion projectile, shamelessly ripped from examplemod
        public override void AI()
        {
            CheckActive();
            Behavior();
        }

        public abstract void CheckActive();

        public abstract void Behavior();
    }
}