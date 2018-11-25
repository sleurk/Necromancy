using Microsoft.Xna.Framework;

namespace Necromancy.Projectiles
{
    public class EnergyTeal : WandEnergy
    {
        // basic projectile

        protected override int Pierce
        {
            get { return 1; }
        }

        protected override int Heal
        {
            get { return 3; }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Teal Energy");
        }

        protected override Color GetColor()
        {
            return new Color(0, .9f, .75f);
        }
    }
}